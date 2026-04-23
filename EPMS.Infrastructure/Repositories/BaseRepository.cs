using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using EPMS.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class BaseRepository<T, TKey> : IBaseRepository<T, TKey> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(TKey id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            throw HandleDbUpdateException(ex, entity);
        }
    }

    private Exception HandleDbUpdateException(DbUpdateException ex, T entity)
    {
        var innerException = ex.InnerException;
        if (innerException?.Message.Contains("REFERENCE constraint") == true)
        {
            var entityName = typeof(T).Name;
            var keyProperty = _dbSet.EntityType.FindPrimaryKey()?.Properties.FirstOrDefault();
            var entityId = keyProperty != null
                ? entity.GetType().GetProperty(keyProperty.Name)?.GetValue(entity) ?? 0
                : 0;

            return new RelatedEntityExistsException(
                entityName,
                entityId,
                "related",
                0
            );
        }

        return new EntityConstraintException($"Error deleting {typeof(T).Name}: {ex.Message}", ex);
    }
}
