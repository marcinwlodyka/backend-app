using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend_app.Data;
using backend_app.Models;
using Microsoft.AspNetCore.Authorization;

namespace backend_app.Controllers;

public class MovieMutation
{
    public string? Title { get; set; }
    public string? ReleaseDate { get; set; }

    public int? GenreId { get; set; }

    public IEnumerable<int>? ActorIds { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly AppDbContext _context;

    public MovieController(AppDbContext context)
    {
        _context = context;
    }

    private IQueryable<Movie> _GetMovies() => _context.Movies
        .Include(m => m.Genre)
        .Select(m => new Movie()
        {
            Id = m.Id,
            Title = m.Title,
            Genre = m.Genre,
            ReleaseDate = m.ReleaseDate,
            Actors = _context.Movies
                .Where(m2 => m2.Id == m.Id)
                .SelectMany(m2 => m2.Actors)
                .ToArray()
        });

    // GET: api/MovieApi
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        if (_context.Movies == null)
            return NotFound();

        return await _GetMovies().ToListAsync();
    }

    // GET: api/MovieApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        if (_context.Movies == null)
            return NotFound();

        var movie = _GetMovies().First(m => m.Id == id);

        return movie == null ? NotFound() : movie;
    }

    // PUT: api/MovieApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutMovie(int id, MovieMutation movie)
    {
        var foundMovie = _context.Movies
            .Include(m => m.Actors)
            .Single(m => m.Id == id);

        if (foundMovie == null) return BadRequest();

        if (movie.ActorIds != null && movie.ActorIds.Any())
        {
            foundMovie.Actors = _context.Actors.Where(a => movie.ActorIds.Contains(a.Id)).ToArray();
        }

        foundMovie.Title = movie.Title ?? foundMovie.Title;
        foundMovie.ReleaseDate = movie.ReleaseDate == null ? foundMovie.ReleaseDate : DateTime.Parse(movie.ReleaseDate);
        foundMovie.Genre = _context.Genres.Find(movie.GenreId) ?? foundMovie.Genre;

        _context.Entry(foundMovie).State = EntityState.Modified;

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

    // POST: api/MovieApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Movie>> PostMovie(MovieMutation movie)
    {
        if (_context.Movies == null)
        {
            return Problem("Entity set 'AppDbContext.Movies'  is null.");
        }

        var newMovie = new Movie()
        {
            Title = movie.Title,
            Genre = await _context.Genres.FindAsync(movie.GenreId),
            ReleaseDate = DateTime.Parse(movie.ReleaseDate),
            Actors = _context.Actors.Where(a => movie.ActorIds != null && movie.ActorIds.Contains(a.Id)).ToArray()
        };

        _context.Movies.Add(newMovie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = newMovie.Id }, movie);
    }

    // DELETE: api/MovieApi/5
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        if (_context.Movies == null)
        {
            return NotFound();
        }

        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieExists(int id)
    {
        return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}