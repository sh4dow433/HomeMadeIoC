using System.ComponentModel.DataAnnotations;

namespace ToDoList;

public class ToDoTask
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsDone { get; set; } = false;
}

