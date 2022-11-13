
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

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbContext.Set<T>().Where(filter).ToListAsync();

    }

    public async Task<T?> GetAsync(int id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
