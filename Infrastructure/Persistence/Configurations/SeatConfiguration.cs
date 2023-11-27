using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seats")
            .HasKey(s => s.SeatId);

        builder
            .Property(s => s.SeatId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(s => s.Row)
            .IsRequired();

        builder
            .Property(s => s.Column)
            .IsRequired();

        builder
            .Property(s => s.SeatType)
            .IsRequired();

        builder
            .HasOne<Cinema>(s => s.Cinema)
            .WithMany(c => c.Seats)
            .IsRequired();

        builder
            .HasMany<SeatReservation>(s => s.SeatReservations)
            .WithOne(sr => sr.Seat)
            .IsRequired();
    }
}