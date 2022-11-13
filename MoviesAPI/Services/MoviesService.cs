using MoviesAPI.DataAccess;
using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services;

public class MoviesService : IMoviesService
{
    private readonly IGenericRepo<Movie> _repo;

    public MoviesService(IGenericRepo<Movie> repo)
    {
        _repo = repo;
    }
    public async Task<(Movie?, List<ValidationResult>?)> CreateMovieAsync(Movie movie)
    {
        if (DbValidator.IsValid(movie, out var validationResults))
        {
            return (await _repo.CreateAsync(movie), null);
        }
        return (null, validationResults);
    }

    public async Task<(Movie?, List<ValidationResult>?)> UpdateMovieAsync(Movie movie)
    {
        if (DbValidator.IsValid(movie, out var validationResults))
        {
            return (await _repo.UpdateAsync(movie), null);
        }
        return (null, validationResults);
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync(bool includeDirectors = false)
    {
        if (includeDirectors)
        {
            return await _repo.GetAllAsync(new List<string> { "Directors" });
        }
        return await _repo.GetAllAsync();
    }
    public async Task<IEnumerable<Movie>> GetAllMoviesByTitleAsync(string title, bool includeDirectors = false)
    {

        if (includeDirectors)
        {
            return await _repo.GetAllAsync(m => m.Title.Contains(title), new List<string> { "Directors" });
        }
        return await _repo.GetAllAsync(m => m.Title.Contains(title));
    }

    public async Task<Movie?> GetMovieByIdAsync(int id, bool includeDirectors = false)
    {

        if (includeDirectors)
        {
            return await _repo.GetAsync(id, new List<string> { "Directors" });
        }
        return await _repo.GetAsync(id);
    }

    public async Task<Movie?> DeleteMovieAsync(int id)
    {
        return await _repo.DeleteAsync(id);
    }
}
