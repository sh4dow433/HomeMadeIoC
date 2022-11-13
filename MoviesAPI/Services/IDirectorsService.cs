using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services
{
    public interface IDirectorsService
    {
        Task<(Director?, List<ValidationResult>?)> CreateDirectorAsync(Director director);
        Task<Director?> DeleteDirectorAsync(int id);
        Task<IEnumerable<Director>> GetDirectorsAsync(bool includeMovies = false);
        Task<Director?> GetDirectorsByIdAsync(int id, bool includeMovies = false);
        Task<IEnumerable<Director>> GetDirectorsByNameAsync(string name, bool includeMovies = false);
        Task<(Director?, List<ValidationResult>?)> UpdateDirectorAsync(Director director);
    }
}