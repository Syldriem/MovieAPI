using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieCardsAPI.Models.Entities;
using System.Globalization;
using System.Net;

namespace MovieCardsAPI.Data
{
    public class SeedData
    {
        private static Faker faker = new Faker("sv");
        internal static async Task InitAsync(MovieCardsAPIContext context)
        {
            if (await context.Movie.AnyAsync()) return;

            var movies = GenerateMovies(100);
            await context.AddRangeAsync(movies);

            await context.SaveChangesAsync();

        }

        private static List<Actor> GenerateActors(int numberOfActors)
        {
            var actors = new List<Actor>(numberOfActors);
            for (int i = 0; i < numberOfActors; i++)
            {
                var actorName = faker.Name.FullName();
                var actorBirthdate = faker.Date.Between(new DateTime(1940, 1, 1), new DateTime(2000, 1, 1));
                var actor = new Actor
                {
                    Name = actorName,
                    Birthdate = actorBirthdate
                };
                actors.Add(actor);
            }

            return actors;
        }

        private static List<Genre> GenerateGenres()
        {
            var genres = new List<Genre>();
            var genreNames = new List<string> { "Action", "Comedy", "Drama", "Horror", "Sci-Fi", "Thriller" };

            for (int i = 0; i < genreNames.Count; i++)
            { 
                var genre = new Genre
                {
                    Name = genreNames[i],
                };
                genres.Add(genre);
            }
            return genres;


        }

        private static List<Director> GenerateDirectors(int directorAmount)
        {
            var faker = new Faker<Director>("sv").Rules((faker, director) =>
            {
                director.Name= faker.Name.FirstName();
                director.Birthdate = faker.Date.Past(80, DateTime.Now.AddYears(-50));
                director.ContactInformation = new ContactInformation
                {
                    Email = faker.Internet.Email(),
                    phoneNumber = faker.Random.Number(10000000, 99999999)
                };
            });

            return faker.Generate(directorAmount);
        }

        private static IEnumerable<Movie> GenerateMovies(int movieAmount)
        {
            var movies = new List<Movie>(movieAmount);
            var director = GenerateDirectors(20);
            var actors = GenerateActors(20);
            var genres = GenerateGenres();

            for (int i = 0; i < movieAmount; i++)
            {
                var movieName = faker.Random.Words(faker.Random.Number(1, 3));
                var movieRating = faker.Random.Int(1, 10).ToString();
                var movieDesc = faker.Lorem.Paragraph(1);
                var movieReleaseDate = faker.Date.Between(new DateTime(2000, 1, 1), new DateTime(2021, 1, 1));

                var movie = new Movie
                {
                    Title = movieName,
                    Rating = movieRating,
                    Description = movieDesc,
                    ReleaseDate = movieReleaseDate,
                    Director = director[faker.Random.Number(0, director.Count - 1)],
                    Actors = actors.Take(faker.Random.Number(1, actors.Count -1)).ToList(),
                    Genres = genres.Take(faker.Random.Number(1, genres.Count - 1)).ToList()

                };

                movies.Add(movie);

            }
            return movies;
        }
    }

}
