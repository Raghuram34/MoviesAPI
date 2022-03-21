using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;

        public ActorController(IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }

        [HttpGet]
        public List<Actor> GetActors()
        {
            return movieAPIService.GetActors();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddActor([FromBody] Actor actor)
        {
            // If actor is null, then return
            if (actor == null)
            {
                return BadRequest();
            }

            try
            {
                movieAPIService.CreateActor(actor);
                return StatusCode(StatusCodes.Status201Created, new { message = "Actor is successfully created." });
            }
            catch (Exception ex)
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
