using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;
        private readonly MovieDbContext _movieDbContext;

        public MoviesController (MovieDbContext movieDbContext, IMoviesAPIService _movieAPIService)
        {
            _movieDbContext = movieDbContext;
            movieAPIService = _movieAPIService;
        }

        [HttpGet]
        public ActionResult<List<Movie>> GetAllMovies() 
        {
            return movieAPIService.GetMovies();
        }


        [HttpGet("{movieId}")]
        public ActionResult<Movie> GetMovieById(int movieId)
        {
            var movie = movieAPIService.GetMovieById(movieId);
            if(movie == null)
            {
                return NotFound("No such movie existed with this id");
            }
            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            // If movie is null, then return
            if (movie == null)
            {
                return BadRequest();
            }

            try
            {
                movieAPIService.CreateMovie(movie);
                return StatusCode(StatusCodes.Status201Created, new { message = "Movie is successfully created." });
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        [HttpPut]
        public IActionResult EditMovie([FromBody] Movie movie)
        {
            // If movie is null, then return
            if (movie == null)
            {
                return BadRequest();
            }

            try
            {
                movieAPIService.UpdateMovie(movie);
                return Ok();
            }
            catch(Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        private IActionResult ExceptionHandler(Exception ex)
        {
            switch (ex)
            {
                case KeyNotFoundException:
                    return BadRequest(ex.Message);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
