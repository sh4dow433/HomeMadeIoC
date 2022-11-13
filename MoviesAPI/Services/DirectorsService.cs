using MoviesAPI.DataAccess;
using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services;

public class DirectorsService : IDirectorsService
{
    private readonly IGenericRepo<Director> _repo;

    public DirectorsService(IGenericRepo<Director> repo)
    {
        _repo = repo;
    }

    public async Task<(Director?, List<ValidationResult>?)> CreateDirectorAsync(Director director)
    {
        if (DbValidator.IsValid(director, out var validationResults))
        {
            return (await _repo.CreateAsync(director), null);
        }
        return (null, validationResults);
    }

    public async Task<(Director?, List<ValidationResult>?)> UpdateDirectorAsync(Director director)
    {
        if (DbValidator.IsValid(director, out var validationResults))
        {
            return (await _repo.UpdateAsync(director), null);
        }
        return (null, validationResults);
    }

    public async Task<Director?> DeleteDirectorAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }
    public async Task<IEnumerable<Director>> GetDirectorsAsync(bool includeMovies = false)
    {
        if (includeMovies)
        {
            return await _repo.GetAllAsync(new List<string> { "Movies" });
        }
        return await _repo.GetAllAsync();
    }
    public async Task<IEnumerable<Director>> GetDirectorsByNameAsync(string name, bool includeMovies = false)
    {
        if (includeMovies)
        {
            return await _repo.GetAllAsync(d => d.Name.Contains(name), new List<string> { "Movies" });
        }
        return await _repo.GetAllAsync(d => d.Name.Contains(name));
    }

    public async Task<Director?> GetDirectorsByIdAsync(int id, bool includeMovies = false)
    {
        if (includeMovies)
        {
            return await _repo.GetAsync(id, new List<string> { "Movies" });
        }
        return await _repo.GetAsync(id);
    }
}
