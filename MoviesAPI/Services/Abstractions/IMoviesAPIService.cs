using MoviesAPI.Models;

namespace MoviesAPI.Services.Abstractions
{
    public interface IMoviesAPIService
    {
        
        List<Movie> GetMovies();

        Movie GetMovieById(int id);
        
        void CreateMovie(Movie movie);

        void UpdateMovie(Movie movie);

        List<Actor> GetActors();

        void CreateActor(Actor actor);

        List<Producer> GetProducers();

        void CreateProducer(Producer producer);
    }
}
