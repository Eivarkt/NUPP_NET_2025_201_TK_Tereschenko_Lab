using System.Text.Json;
using ClothingStore.Common.Abstractions;

namespace ClothingStore.Common.Services;

public class CrudService<T> : ICrudService<T> where T : IHasId
{
    private readonly List<T> _items = new();

    public void Create(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        bool alreadyExists = _items.Any(item => item.Id == element.Id);
        if (alreadyExists)
        {
            throw new InvalidOperationException($"Element with id {element.Id} already exists.");
        }

        _items.Add(element);
    }

    public T Read(Guid id)
    {
        return _items.FirstOrDefault(item => item.Id == id)
            ?? throw new KeyNotFoundException($"Element with id {id} was not found.");
    }

    public IEnumerable<T> ReadAll()
    {
        return _items.ToList();
    }

    public void Update(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        int index = _items.FindIndex(item => item.Id == element.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Element with id {element.Id} was not found.");
        }

        _items[index] = element;
    }

    public void Remove(T element)
    {
        if (element is null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        T existingElement = Read(element.Id);
        _items.Remove(existingElement);
    }

    // Додаткове завдання: збереження даних у файл
    public void Save(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_items, options);
        File.WriteAllText(filePath, json);
    }

    // Додаткове завдання: завантаження даних із файлу
    public void Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Data file was not found.", filePath);
        }

        string json = File.ReadAllText(filePath);
        List<T>? loadedItems = JsonSerializer.Deserialize<List<T>>(json);

        _items.Clear();
        if (loadedItems is not null)
        {
            _items.AddRange(loadedItems);
        }
    }
}
