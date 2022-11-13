using HomeMadeIoC.Api;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DataAccess;
using MoviesAPI.Entities;
using MoviesAPI.Services;
using Newtonsoft.Json;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 


// Add dependencies to the IoC contianer
var homemadeIoC = new Container();
// homemadeIoC.AddServicesFromConfigurationFile(Environment.CurrentDirectory + "/services.json");

homemadeIoC.AddSingleton<ConfigurationBuilder>();
homemadeIoC.AddScoped<DbContext, AppDbContext>();
homemadeIoC.AddScoped<IGenericRepo<Movie>, GenericRepo<Movie>>();
homemadeIoC.AddScoped<IGenericRepo<Director>, GenericRepo<Director>>();
homemadeIoC.AddScoped<IDirectorsService, DirectorsService>();
homemadeIoC.AddScoped<IMoviesService, MoviesService>();

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
app.MapGet("/movies", async (HttpRequest request) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesAsync(request.Query["includeDirector"].ToString().ToUpper() == "TRUE");
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No movie could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/movies/bytitle/{title}", async (string title, HttpRequest request) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    IEnumerable<Movie> result;
    result = await moviesService.GetAllMoviesByTitleAsync(title, request.Query["includeDirector"].ToString().ToUpper() == "TRUE");
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No movie could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/movies/{id:int}", async (int id, HttpRequest request) =>
{
    var moviesService = homemadeIoC.GetService<IMoviesService>();
    Movie? result;
    result = await moviesService.GetMovieByIdAsync(id, request.Query["includeDirector"].ToString().ToUpper() == "TRUE");
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
app.MapGet("/directors", async (HttpRequest request) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    IEnumerable<Director> result;
    result = await directorsService.GetDirectorsAsync(request.Query["includeMovies"].ToString().ToUpper() == "TRUE");
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No director could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/directors/byname/{name}", async (string name, HttpRequest request) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    IEnumerable<Director> result;
    result = await directorsService.GetDirectorsByNameAsync(name, request.Query["includeMovies"].ToString().ToUpper() == "TRUE");
    if (result == null || result.Any() == false)
    {
        return Results.NotFound("No director could be found");
    }
    return Results.Ok(result);
});

app.MapGet("/directors/{id:int}", async (int id, HttpRequest request) =>
{
    var directorsService = homemadeIoC.GetService<IDirectorsService>();
    Director? result;
    result = await directorsService.GetDirectorsByIdAsync(id, request.Query["includeMovies"].ToString().ToUpper() == "TRUE");
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

