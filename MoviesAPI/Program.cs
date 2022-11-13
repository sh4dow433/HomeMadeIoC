using HomeMadeIoC.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DataAccess;
using MoviesAPI.Entities;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var homemadeIoC = new Container();

homemadeIoC.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/services.json");

homemadeIoC.AddSingleton<ConfigurationBuilder>();

homemadeIoC.AddScoped<DbContext, AppDbContext>();
homemadeIoC.AddScoped<IGenericRepo<Movie>, GenericRepo<Movie>>();
homemadeIoC.AddScoped<IGenericRepo<Director>, GenericRepo<Director>>();
homemadeIoC.AddScoped<IDirectorsService, DirectorsService>();
homemadeIoC.AddScoped<IMoviesService, MoviesService>();

var dbcontext = homemadeIoC.GetService<DbContext>();
dbcontext.Database.Migrate();

var app = builder.Build();

app.UseHttpsRedirection();

// MOVIES API ROUTES:
app.MapGet("/movies", async ([FromQuery] bool includeDirector) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesAsync(includeDirector);
    if (result == null || result.Any() == false)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});

app.MapGet("/movies/bytitle/{title}", async (string title, [FromQuery] bool includeDirector) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesByTitleAsync(title, includeDirector);
    if (result == null || result.Any() == false)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});

app.MapGet("/movies/{id:int}", async (int id, [FromQuery] bool includeDirector) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    Movie? result;
    result = await moviesService.GetMovieByIdAsync(id, includeDirector);
    if (result == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});



app.MapPost("/movies", async (Movie movie) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
});

app.MapPut("/movies/{id:int}", async (int id, Movie movie) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
});
app.MapDelete("/movies/{id:int}", async (int id) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    var result = await moviesService.DeleteMovieAsync(id);
    if (result == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});


// DIRECTORS API ROUTES:

app.Run();

