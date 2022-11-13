using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities;
public class Director : Entity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public int Age { get; set; }
    public List<Movie> Movies { get; set; } = new();

}