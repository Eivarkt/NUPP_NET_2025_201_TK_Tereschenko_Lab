using ClothingStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ClothingStoreContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ClothingStoreContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        T? entity = await _dbSet.FindAsync(id);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Entity with id {id} was not found.");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
