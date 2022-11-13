using MoviesAPI.DataAccess;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class MoviesService
{
    public MoviesService(IGenericRepo<Movie> repo)
    {

    }
    Task<Movie> CreateMovieAsync(Movie movie)
    {
        throw new NotImplementedException()
        {

        };
    }
}
