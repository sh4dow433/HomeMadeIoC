using Newtonsoft.Json;
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
    
    [ForeignKey(nameof(DirectorId))]
    public Director Director { get; set; } = null!;
    [Required]
    public int DirectorId { get; set; }

}
