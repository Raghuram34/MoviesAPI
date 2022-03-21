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

        public MoviesController (IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }

        [HttpGet]
        public IActionResult GetAllMovies() 
        {
            try
            {
                var result = movieAPIService.GetMovies();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return ExceptionHandler(ex, ex.Message);
            }
            
        }


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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            // If movie is null or model is invalid, then return
            if (movie == null || !ModelState.IsValid)
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
                return BadRequest("Invalid Model");
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            // If movie is null, then return
            if (movie == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                movieAPIService.UpdateMovie(movie);
                return Accepted("Updated the movie");
            }
            catch(Exception ex)
            {
                return ExceptionHandler(ex, ex.Message);
            }
        }

        private IActionResult ExceptionHandler(Exception ex, string message)
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
