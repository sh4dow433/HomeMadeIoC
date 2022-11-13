using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<(Movie?, List<ValidationResult>?)> CreateMovieAsync(Movie movie);
        Task<(Movie?, List<ValidationResult>?)> UpdateMovieAsync(Movie movie);
        Task<Movie?> DeleteMovieAsync(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsync(bool includeDirectors = false);
        Task<IEnumerable<Movie>> GetAllMoviesByTitleAsync(string title, bool includeDirectors = false);
        Task<Movie?> GetMovieByIdAsync(int id, bool includeDirectors = false);
    }
}