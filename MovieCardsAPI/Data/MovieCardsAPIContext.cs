using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieCardsAPI.Models.Dtos;
using MovieCardsAPI.Models.Entities;

    public class MovieCardsAPIContext : DbContext
    {
        public MovieCardsAPIContext(DbContextOptions<MovieCardsAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie => Set<Movie>();
        public DbSet<Genre> Genre => Set<Genre>();
        public DbSet<Actor> Actor => Set<Actor>();
        public DbSet<Director> Director => Set<Director>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie
            //    {
            //        Id = 1,
            //        Title = "The Shawshank Redemption",
            //        Rating = "9.3",
            //        ReleaseDate = new DateTime(1994, 10, 14),
            //        Description = "Two imprisoned"
            //    }
            //);
        }
    }
