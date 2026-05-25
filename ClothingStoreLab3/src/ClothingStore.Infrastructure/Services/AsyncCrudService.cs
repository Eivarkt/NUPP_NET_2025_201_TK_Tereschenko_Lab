using ClothingStore.Common.Abstractions;
using ClothingStore.Infrastructure.Models;
using ClothingStore.Infrastructure.Repositories;

namespace ClothingStore.Infrastructure.Services;

public class AsyncCrudService<T> : ICrudServiceAsync<T> where T : EntityBase
{
    private readonly IRepository<T> _repository;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public AsyncCrudService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            await _repository.AddAsync(element);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<T> ReadAsync(Guid id)
    {
        await _semaphore.WaitAsync();
        try
        {
            IEnumerable<T> elements = await _repository.GetAllAsync();
            return elements.FirstOrDefault(element => element.PublicId == id)
                ?? throw new KeyNotFoundException($"Entity with public id {id} was not found.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            return (await _repository.GetAllAsync()).ToList();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        if (page <= 0)
        {
            throw new ArgumentException("Page must be greater than zero.", nameof(page));
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        }

        await _semaphore.WaitAsync();
        try
        {
            IEnumerable<T> elements = await _repository.GetAllAsync();

            return elements
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToList();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> UpdateAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            await _repository.Update(element);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> RemoveAsync(T element)
    {
        await _semaphore.WaitAsync();
        try
        {
            await _repository.Delete(element);
            return true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public Task<bool> SaveAsync()
    {
        // Entity Framework saves changes through SaveChangesAsync() inside repository methods.
        return Task.FromResult(true);
    }
}
