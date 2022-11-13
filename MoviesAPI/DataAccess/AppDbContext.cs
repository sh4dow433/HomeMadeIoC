using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoviesAPI.Entities;

namespace MoviesAPI.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }

    private readonly IConfiguration _configuration;
    public AppDbContext(ConfigurationBuilder configurationBuilder)
    {
        _configuration = configurationBuilder
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();


        Movies = Set<Movie>();
        Directors = Set<Director>();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("sqlitedb"));
    }
}
