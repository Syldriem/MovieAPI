﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCardsAPI.Models.Dtos;
using MovieCardsAPI.Models.Entities;

namespace MovieCardsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCardsAPIContext _context;

        public MoviesController(MovieCardsAPIContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovie()
        {
            //return await _context.Movie.ToListAsync();
            var dto = _context.Movie.Select(m => new MovieDto(m.Id, m.Title, m.Rating, m.ReleaseDate, m.Description, m.DirectorId, m.Director.Name));

            return Ok(await dto.ToListAsync());
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovieDto>> PostMovie(CreateMovieDto dto)
        {
            var genres = await _context.Genre.Where(g => dto.GenreIds.Contains(g.Id)).ToListAsync();
            if (genres.Count != dto.GenreIds.Count())
            {
                return BadRequest("Invalid genre id(s).");
            }
            var director = await _context.Director.FindAsync(dto.DirectorId);
            if (director == null)
            {
                director = new Director { Id = dto.DirectorId };
            }

            var movie = new Movie
            {
                Title = dto.Title,
                Rating = dto.Rating,
                ReleaseDate = dto.ReleaseDate,
                Description = dto.Description,
                DirectorId = dto.DirectorId,
                Director = director,
                Genres = genres,
            };
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();
            var movieDTO = new MovieDto(movie.Id, movie.Title, movie.Rating, movie.ReleaseDate, movie.Description, movie.DirectorId);

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movieDTO);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}
