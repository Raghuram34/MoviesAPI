using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Filters;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;

namespace MoviesAPI.Controllers
{
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;

        public ActorController(IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }
        
        // GET: /api/{controller}
        [HttpGet]
        public List<Actor> GetActors()
        {
            var response = movieAPIService.GetActors();
            return response;
        }


        // POST: /api/{controller}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            // If actor is null, then return
            if (actor == null)
            {
                return BadRequest();
            }

            movieAPIService.CreateActor(actor);
            return StatusCode(StatusCodes.Status201Created, new { message = "Actor is successfully created." });
        }
    }
}
