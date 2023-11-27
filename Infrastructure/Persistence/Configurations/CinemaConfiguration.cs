using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations;

public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
{
    public void Configure(EntityTypeBuilder<Cinema> builder)
    {
        builder.ToTable("Cinemas")
            .HasKey(c => c.CinemaId);

        builder
            .Property(c => c.CinemaId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(c => c.BaseTicketPrice)
            .IsRequired();

        builder
            .Property(c => c.OpeningAtUtc)
            .IsRequired();

        builder
            .Property(c => c.ClosingAtUtc)
            .IsRequired();

        builder
            .HasMany<MovieSession>(c => c.MovieSessions)
            .WithOne(ms => ms.Cinema)
            .IsRequired();

        builder
            .HasMany<Seat>(c => c.Seats)
            .WithOne(s => s.Cinema)
            .IsRequired();
    }
}