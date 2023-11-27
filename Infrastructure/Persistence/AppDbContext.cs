// using Collections.Domain.Entities;
// using Cinebook.Infrastructure.Persistence.Configurations;

using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Cinema section
    public DbSet<Cinema> Cinemas => Set<Cinema>();
    public DbSet<MovieSession> MovieSessions => Set<MovieSession>();

    // Seat section
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<SeatReservation> SeatReservations => Set<SeatReservation>();

    // Movie section
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}