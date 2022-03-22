using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;

namespace MoviesAPI.Services
{
    /** <summary>
     * Implements the interface IMoviesAPIService
     * </summary>
     */
    public class MoviesAPIService : IMoviesAPIService
    {

        private readonly MovieDbContext movieDbContext;
        public MoviesAPIService(MovieDbContext _movieDbContext)
        {
            movieDbContext = _movieDbContext;
        }

        #region Movies Actions
        public List<Movie> GetMovies()
        {
            var res = movieDbContext.Movies
                        .Include(p => p.Producer)
                        .Include(a => a.Actors);
            return res.ToList();
        }

        public Movie GetMovieById(int id)
        {
            return movieDbContext.Movies
                    .Include(p => p.Producer)
                    .Include(a => a.Actors)
                    .FirstOrDefault();
        }

        public void CreateMovie(Movie movie)
        {
            movie.Producer = movieDbContext.Producers
                                .Include(p => p.Movies)
                                .Where(p => movie.Producer.ProducerId == p.ProducerId)
                                .FirstOrDefault();
            // Load Actors whose profile is created. 
            movie.Actors = FindActorsById(movie.Actors.ToList());
            movieDbContext.Movies.Add(movie);
            movieDbContext.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            // get existing object from database
            var movieToUpdate = movieDbContext.Movies.FirstOrDefault(m => m.MovieId == movie.MovieId);

            // If movie not existed, then throw exception
            if(movieToUpdate == null)
            {
                throw new KeyNotFoundException("No such movie found with this Id");
            }
            // Find producer object by id in database to map correctly
            movie.Producer = FindProducerById(movie.Producer);

            // Find actors by id in database to map correctly
            movie.Actors = FindActorsById(movie.Actors.ToList());

            // Set existing object vales with modified object values
            movieDbContext.Entry(movieToUpdate).CurrentValues.SetValues(movie);
            movieToUpdate.Actors = movie.Actors;
            movieToUpdate.Producer = movie.Producer;
            movieDbContext.Entry(movieToUpdate).State = EntityState.Modified;
            movieDbContext.SaveChanges();
        }
        #endregion

        #region Actor Actions
        public List<Actor> GetActors()
        {
            return movieDbContext.Actors.Include(a => a.Movies).ToList();
        }

        public void CreateActor(Actor actor)
        {
            movieDbContext.Actors.Add(actor);
            movieDbContext.SaveChanges();
        }
        #endregion

        #region Producer Actions
        public List<Producer> GetProducers()
        {
            return movieDbContext.Producers.Include(a => a.Movies).ToList();
        }

        public void CreateProducer(Producer producer)
        {
            movieDbContext.Producers.Add(producer);
            movieDbContext.SaveChanges();
        }
        #endregion

        #region Private Methods
        private List<Actor> FindActorsById(List<Actor> actors)
        {
            var actorsList = new List<Actor>();
            foreach (var actor in actors)
            {
                var obj = movieDbContext.Actors
                            .Include(a => a.Movies)
                            .Where(a => actor.ActorId == a.ActorId)
                            .FirstOrDefault();
                if (obj != null)
                {
                    actorsList.Add(obj);
                }
            }
            return actorsList;
        }

        private Producer FindProducerById(Producer producer)
        {
            return movieDbContext.Producers.FirstOrDefault(p => p.ProducerId == producer.ProducerId);
        }
        #endregion
    }
}
