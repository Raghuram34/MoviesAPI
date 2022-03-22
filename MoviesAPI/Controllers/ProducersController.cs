using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Filters;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;

namespace MoviesAPI.Controllers
{
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;

        public ProducersController(IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }

        // GET: /api/{controller}
        [HttpGet]
        public List<Producer> GetProducers()
        {
            return movieAPIService.GetProducers();
        }

        // POST: /api/{controller}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddActor([FromBody] Producer producer)
        {
            // If actor is null, then return
            if (producer == null)
            {
                return BadRequest();
            }

            movieAPIService.CreateProducer(producer);
            return StatusCode(StatusCodes.Status201Created, new { message = "Producer is successfully created." });
        }
    }
}
