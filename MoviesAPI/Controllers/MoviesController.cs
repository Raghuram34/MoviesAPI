using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;
using MoviesAPI.Filters;

namespace MoviesAPI.Controllers
{
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;

        public MoviesController (IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }
        
        // GET: /api/{controller}
        [HttpGet]
        public IActionResult GetAllMovies() 
        {
            var result = movieAPIService.GetMovies();
            return Ok(result);         
        }

        // GET: /api/{controller}/{movieId}
        [HttpGet("{movieId}")]
        public IActionResult GetMovieById(int movieId)
        {
            var movie = movieAPIService.GetMovieById(movieId);
            if(movie == null)
            {
                return NotFound("No such movie existed with this id");
            }
            return Ok(movie);
        }

        // POST: /api/{controller}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            // If movie is null or model is invalid, then return
            if (movie == null || !ModelState.IsValid)
            {
                return BadRequest("Movie Object is either null or Invalid");
            }

            movieAPIService.CreateMovie(movie);
            return StatusCode(StatusCodes.Status201Created, new { message = "Movie is successfully created." });
        }

        // PUT: /api/{controller}
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            // If movie is null, then return
            if (movie == null || !ModelState.IsValid)
            {
                return BadRequest("Movie Object is either null or Invalid");
            }

            movieAPIService.UpdateMovie(movie);
            return Accepted("Updated the movie");
        }
    }
}
