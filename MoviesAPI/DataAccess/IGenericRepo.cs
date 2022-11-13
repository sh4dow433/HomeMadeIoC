using MoviesAPI.Entities;
using System.Linq.Expressions;

namespace MoviesAPI.DataAccess;

public interface IGenericRepo<T> where T : Entity, new()
{
    Task<T> CreateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetAsync(int id);
    Task<T?> DeleteAsync(int id);
    Task<T?> UpdateAsync(T entity);
}
