using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Models;
using MoviesAPI.Services.Abstractions;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IMoviesAPIService movieAPIService;

        public ProducersController(IMoviesAPIService _movieAPIService)
        {
            movieAPIService = _movieAPIService;
        }

        [HttpGet]
        public List<Producer> GetProducers()
        {
            return movieAPIService.GetProducers();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddActor([FromBody] Producer producer)
        {
            // If actor is null, then return
            if (producer == null)
            {
                return BadRequest();
            }

            try
            {
                movieAPIService.CreateProducer(producer);
                return StatusCode(StatusCodes.Status201Created, new { message = "Producer is successfully created." });
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
