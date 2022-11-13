using HomeMadeIoC.Api;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DataAccess;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using Newtonsoft.Json;
//using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 


// Add dependencies to the IoC contianer
var homemadeIoC = new Container();

// You can add the dependencies manually here
//homemadeIoC.AddSingleton<ConfigurationBuilder>();
//homemadeIoC.AddScoped<DbContext, AppDbContext>();
//homemadeIoC.AddScoped<IGenericRepo<Movie>, GenericRepo<Movie>>();
//homemadeIoC.AddScoped<IGenericRepo<Director>, GenericRepo<Director>>();
//homemadeIoC.AddScoped<IDirectorsService, DirectorsService>();
//homemadeIoC.AddScoped<IMoviesService, MoviesService>();

// Or you can use a configuration file to add the dependencies:
homemadeIoC.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/services.json");

var dbcontext = homemadeIoC.GetService<DbContext>();
dbcontext.Database.Migrate(); // apply migrations

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

#region MOVIES
// MOVIES API ROUTES:
app.MapGet("/movies", async () =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesAsync();
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No movie could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/movies/bytitle/{title}", async (string title) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesByTitleAsync(title);
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No movie could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/movies/{id:int}", async (int id) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    Movie? result;
    result = await moviesService.GetMovieByIdAsync(id);
    if (result == null)
    {
        return Results.NotFound("No movie could be found for the given id");
    }
    return Results.Ok(result);
});

app.MapPost("/movies", async (Movie movie) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    var (result, errors) = await moviesService.CreateMovieAsync(movie);
    if (errors != null)
    {
        return Results.BadRequest(errors);
    }
    return Results.Ok(result);
});

app.MapPut("/movies/{id:int}", async (int id, Movie movie) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    movie.Id = id;
    var (result, errors) = await moviesService.UpdateMovieAsync(movie);
    if (errors != null)
    {
        return Results.BadRequest(errors);
    }
    return Results.Ok(result);
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
#endregion

#region DIRECTORS
// DIRECTORS API ROUTES:
app.MapGet("/directors", async () =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    IEnumerable<Director> result;
    result = await directorsService.GetDirectorsAsync();
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No director could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/directors/byname/{name}", async (string name) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    IEnumerable<Director> result;
    result = await directorsService.GetDirectorsByNameAsync(name);
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No director could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/directors/{id:int}", async (int id) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    Director? result;
    result = await directorsService.GetDirectorsByIdAsync(id);
    if (result == null)
    {
        return Results.NotFound("No director could be found");
    }
    return Results.Ok(result);
});

app.MapPost("/directors", async (Director director) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    var (result, errors) = await directorsService.CreateDirectorAsync(director);
    if (errors != null)
    {
        return Results.BadRequest(errors);
    }
    return Results.Ok(result);
});

app.MapPut("/directors/{id:int}", async (int id, Director director) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    var (result, errors) = await directorsService.UpdateDirectorAsync(director);
    if (errors != null)
    {
        return Results.BadRequest(errors);
    }
    return Results.Ok(result);
});

app.MapDelete("/directors/{id:int}", async (int id) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    var result = await directorsService.DeleteDirectorAsync(id);
    if (result == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(result);
});
#endregion

app.Run();

