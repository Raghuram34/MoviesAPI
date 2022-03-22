using MoviesAPI.Models;

namespace MoviesAPI.Services.Abstractions
{
    public interface IMoviesAPIService
    {
        /// <summary>
        /// Gets all the movies
        /// </summary>
        /// <returns>List of Movies</returns>
        List<Movie> GetMovies();

        /// <summary>
        /// Gets the movie by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the movie object</returns>
        Movie GetMovieById(int id);

        /// <summary>
        /// Creates the Movie with the given details
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        void CreateMovie(Movie movie);

        /// <summary>
        /// Updates the movie details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void UpdateMovie(Movie movie);

        /// <summary>
        /// Gets all the actors
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of actor details</returns>
        List<Actor> GetActors();

        /// <summary>
        /// Creates the actor with the given details
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        void CreateActor(Actor actor);

        /// <summary>
        /// Gets all the Producers
        /// </summary>
        /// <param name=""></param>
        /// <returns>List of Producer details</returns>
        List<Producer> GetProducers();

        /// <summary>
        /// Creates the producer with the given details
        /// </summary>
        /// <param name="producer"></param>
        /// <returns></returns>
        void CreateProducer(Producer producer);
    }
}
