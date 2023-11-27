using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations;

public class MovieSessionConfiguration : IEntityTypeConfiguration<MovieSession>
{
    public void Configure(EntityTypeBuilder<MovieSession> builder)
    {
        builder
            .ToTable("MovieSessions")
            .HasKey(ms => ms.MovieSessionId);

        builder
            .Property(ms => ms.MovieSessionId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(ms => ms.StartTimeUtc)
            .IsRequired();

        builder
            .Property(ms => ms.EndTimeUtc)
            .IsRequired();

        builder
            .HasOne<Movie>(ms => ms.Movie)
            .WithMany();

        builder
            .HasOne<Cinema>(ms => ms.Cinema)
            .WithMany(c => c.MovieSessions);

        builder
            .HasMany<SeatReservation>(ms => ms.SeatReservations)
            .WithOne(sr => sr.MovieSession);
    }
}