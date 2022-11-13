using Microsoft.EntityFrameworkCore;
namespace ToDoList.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<ToDoTask> Tasks { get; set; }
    public AppDbContext()
    {
        Tasks = Set<ToDoTask>();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource = tasks.db");
    }
}
