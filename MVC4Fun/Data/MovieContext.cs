using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC4Fun.Models;

namespace MVC4Fun.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MVC4Fun.Models.Movie> Movies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MVC4Fun.Models.Movie>().HasData(
                 new Movie
                 {
                     MovieID = 1,
                     Film = "Lilo & Stitch",
                     Year = 2025,
                     Director = "Dean Fleischer Camp"
                 },
                new Movie
                {
                    MovieID = 2,
                    Film = "Kingdom of the Planet of the Apes",
                    Year = 2024,
                    Director = "Wes Ball"
                },
                new Movie
                {
                    MovieID = 3,
                    Film = "Kung Fu Panda 4",
                    Year = 2024,
                    Director = "Mike Mitchell"
                },
                new Movie
                {
                    MovieID = 4,
                    Film = "Oppenheimer",
                    Year = 2023,
                    Director = "Christopher Edward Nolan"
                }
            );
        }
    }
}
