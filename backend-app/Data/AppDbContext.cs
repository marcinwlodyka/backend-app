using backend_app.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Data;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Movie>()
            .HasOne(m => m.Genre)
            .WithMany(g => g.Movies)
            .HasForeignKey("GenreId");

        builder.Entity<Genre>().HasData(
            new Genre() { Id = 1, Name = "action", Description = "💥🔫"},
            new Genre() { Id = 2, Name = "fantasy", Description = "🧙🪄️" },
            new Genre() { Id = 3, Name = "romance", Description = "😘💘" }
        );

        builder.Entity<Movie>().HasData(
            new Movie() { Id = 1, Title = "Spider-man", ReleaseDate = new DateTime(2002, 4, 30), GenreId = 1},
            new Movie() { Id = 2, Title = "Harry Potter", ReleaseDate = new DateTime(2001, 11, 4), GenreId = 2 },
            new Movie() { Id = 3, Title = "Titanic", ReleaseDate = new DateTime(1997, 11, 1), GenreId = 3 }
        );
        
        builder.Entity<Actor>().HasData(
            new Actor() { Id = 1, FirstName = "Tobey", LastName = "Maguire", DateOfBirth = new DateTime(1975, 6, 27) },
            new Actor() { Id = 2, FirstName = "Willem", LastName = "Dafoe", DateOfBirth = new DateTime(1955, 7, 22) },
            new Actor() { Id = 3, FirstName = "Daniel", LastName = "Radcliffe", DateOfBirth = new DateTime(1989, 7, 23) },
            new Actor() { Id = 4, FirstName = "Leonardo", LastName = "DiCaprio", DateOfBirth = new DateTime(1974, 11, 11) }
        );
        
        builder.Entity<Movie>()
            .HasMany(m => m.Actors)
            .WithMany(a => a.Movies)
            .UsingEntity(join => join.HasData(
                new { MoviesId = 1, ActorsId = 1 },   
                new { MoviesId = 1, ActorsId = 2 },   
                new { MoviesId = 2, ActorsId = 3 },
                new { MoviesId = 3, ActorsId = 4 }
            ));
    }
}