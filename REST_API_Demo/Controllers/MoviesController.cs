using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API_Demo.Models;
using REST_API_Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository) => _movieRepository = movieRepository;

        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _movieRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            var movieToGet = await _movieRepository.GetById(id);
            if (movieToGet != null)
                return movieToGet;

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovies(string title, string director, string description = "", float price = 0, int quantity = 1)
        {
            var movie = await _movieRepository.Add(new Movie
            {
                Title = title,
                Director = director,
                Description = description,
                Price = price,
                Quantity = quantity
            });

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + movie.Id, movie);
        }

        [HttpPost("list")]
        public async Task<ActionResult> Fill()
        {
            List<Movie> movies = Scraper.Scrape("https://www.hollywoodreporter.com/lists/100-best-films-ever-hollywood-favorites-818512");
            _movieRepository.Fill(movies);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var movieToDelete = await _movieRepository.GetById(id);
            if (movieToDelete == null)
                return NotFound();

            await _movieRepository.Delete(movieToDelete.Id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await _movieRepository.Clear();
            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> Remove(Guid id, int quantity)
        {
            var movieToRemove = await _movieRepository.GetById(id);
            if (movieToRemove == null)
                return NotFound();

            if (movieToRemove.Quantity < quantity)
                return BadRequest();

            await _movieRepository.Remove(movieToRemove, quantity);
            return Ok();
        }
    }
}
