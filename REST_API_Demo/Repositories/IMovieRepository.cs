using REST_API_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Demo.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> Get();
        Task<Movie> GetById(Guid id);
        Task<Movie> Add(Movie movie);
        Task Remove(Movie movie, int quantity);
        Task Delete(Guid id);
        Task Clear();
        Task Fill(List<Movie> movies);
    }
}
