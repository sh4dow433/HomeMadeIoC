using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities;

public abstract class Entity
{
    [Key]
    public int Id { get; set; }
}
