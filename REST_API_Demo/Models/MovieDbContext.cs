using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API_Demo.Models
{
    public class MovieDbContext : DbContext
    {
        //public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        //{
        //    Database.EnsureCreated();
        //}

        //public DbSet<Movie> Movies { get; set; }      
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
