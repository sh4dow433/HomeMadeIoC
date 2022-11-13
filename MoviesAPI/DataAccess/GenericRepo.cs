
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MoviesAPI.DataAccess;

public class GenericRepo<T> : IGenericRepo<T> where T : Entity, new()
{
    private readonly DbContext _dbContext;


    public GenericRepo(DbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public async Task<T> CreateAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity == null)
        {
            return null;
        }
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync(IEnumerable<string>? includes = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, IEnumerable<string>? includes = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(filter).ToListAsync();
    }

    public async Task<T?> GetAsync(int id, IEnumerable<string>? includes = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
