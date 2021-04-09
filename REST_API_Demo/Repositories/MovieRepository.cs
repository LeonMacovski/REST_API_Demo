using Microsoft.EntityFrameworkCore;
using REST_API_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Demo.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _context;

        public MovieRepository(MovieDbContext context) => _context = context;

        public async Task<Movie> Add(Movie movie)
        {
            var movieToAddOrUpdate = _context.Movies.SingleOrDefault(x => x.Title == movie.Title);
            if (movieToAddOrUpdate == null)
            {
                movie.Id = Guid.NewGuid();
                if (movie.Quantity == 0)
                    movie.Quantity = 1;
                _context.Movies.Add(movie);
                _ = _context.SaveChangesAsync();
            }
            else
            {
                movieToAddOrUpdate.Quantity += movie.Quantity;
                _ = _context.SaveChangesAsync();
            }


            return movie;
        }

        public async Task Clear()
        {
            foreach (var entity in _context.Movies)
                _context.Movies.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var movieToDelete = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task Fill(List<Movie> movies)
        {
            foreach (var movie in movies)
                Add(movie);
        }

        public async Task<IEnumerable<Movie>> Get()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(Guid id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task Remove(Movie movie, int quantity)
        {
            movie.Quantity -= quantity;
            if (movie.Quantity == 0)
                _ = Delete(movie.Id);

            await _context.SaveChangesAsync();

        }
    }
}
