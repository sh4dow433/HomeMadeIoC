using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<(Movie?, List<ValidationResult>?)> CreateMovieAsync(Movie movie);
        Task<(Movie?, List<ValidationResult>?)> UpdateMovieAsync(Movie movie);
        Task<Movie?> DeleteMovieAsync(int id);
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<IEnumerable<Movie>> GetAllMoviesByTitleAsync(string title);
        Task<Movie?> GetMovieByIdAsync(int id);
    }
}