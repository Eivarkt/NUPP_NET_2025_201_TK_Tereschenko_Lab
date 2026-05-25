using System.Collections;
using System.Text.Json;
using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Services;

public class AsyncCrudService<T> : ICrudServiceAsync<T> where T : IHasId
{
    private readonly Dictionary<Guid, T> _items = new();
    private readonly object _lockObject = new();
    private readonly SemaphoreSlim _saveSemaphore = new(1, 1);
    private readonly string _filePath;

    public AsyncCrudService() : this(Path.Combine("data", "items.json"))
    {
    }

    public AsyncCrudService(string filePath)
    {
        _filePath = filePath;
    }

    public Task<bool> CreateAsync(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        lock (_lockObject)
        {
            if (_items.ContainsKey(element.Id))
            {
                return Task.FromResult(false);
            }

            _items.Add(element.Id, element);
            return Task.FromResult(true);
        }
    }

    public Task<T> ReadAsync(Guid id)
    {
        lock (_lockObject)
        {
            if (_items.TryGetValue(id, out T? element))
            {
                return Task.FromResult(element);
            }
        }

        throw new KeyNotFoundException($"Element with id {id} was not found.");
    }

    public Task<IEnumerable<T>> ReadAllAsync()
    {
        lock (_lockObject)
        {
            return Task.FromResult<IEnumerable<T>>(_items.Values.ToList());
        }
    }

    public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        if (page < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        }

        if (amount < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        lock (_lockObject)
        {
            IEnumerable<T> result = _items.Values
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToList();

            return Task.FromResult(result);
        }
    }

    public Task<bool> UpdateAsync(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        lock (_lockObject)
        {
            if (!_items.ContainsKey(element.Id))
            {
                return Task.FromResult(false);
            }

            _items[element.Id] = element;
            return Task.FromResult(true);
        }
    }

    public Task<bool> RemoveAsync(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        lock (_lockObject)
        {
            return Task.FromResult(_items.Remove(element.Id));
        }
    }

    public async Task<bool> SaveAsync()
    {
        await _saveSemaphore.WaitAsync();

        try
        {
            List<T> snapshot;

            lock (_lockObject)
            {
                snapshot = _items.Values.ToList();
            }

            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(snapshot, options);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
        finally
        {
            _saveSemaphore.Release();
        }
    }

    public async Task<bool> LoadAsync()
    {
        if (!File.Exists(_filePath))
        {
            return false;
        }

        string json = await File.ReadAllTextAsync(_filePath);
        List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);

        lock (_lockObject)
        {
            _items.Clear();

            if (loadedItems is not null)
            {
                foreach (T item in loadedItems)
                {
                    _items[item.Id] = item;
                }
            }
        }

        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        lock (_lockObject)
        {
            return _items.Values.ToList().GetEnumerator();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
