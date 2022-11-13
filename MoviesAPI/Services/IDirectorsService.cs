using MoviesAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Services
{
    public interface IDirectorsService
    {
        Task<(Director?, List<ValidationResult>?)> CreateDirectorAsync(Director director);
        Task<Director?> DeleteDirectorAsync(int id);
        Task<IEnumerable<Director>> GetDirectorsAsync();
        Task<Director?> GetDirectorsByIdAsync(int id);
        Task<IEnumerable<Director>> GetDirectorsByNameAsync(string name);
        Task<(Director?, List<ValidationResult>?)> UpdateDirectorAsync(Director director);
    }
}