using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Entities;

public class Movie : Entity
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public int DurationInMinutes { get; set; }
    public string? Description { get; set; }
    
    [Required]
    [ForeignKey(nameof(DirectorId))]    
    public Director Director { get; set; } = null!;
    public int DirectorId { get; set; }

}

public class Director : Entity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Age { get; set; }
    public List<Movie> Movies { get; set; } = new();

}