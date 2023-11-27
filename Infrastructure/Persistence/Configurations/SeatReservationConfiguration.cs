using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations
{
    public class SeatReservationConfiguration : IEntityTypeConfiguration<SeatReservation>
    {
        public void Configure(EntityTypeBuilder<SeatReservation> builder)
        {
            builder
                .ToTable("SeatReservations")
                .HasKey(sr => sr.SeatReservationId);

            builder
                .Property(sr => sr.SeatReservationId)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(sr => sr.IsBooked)
                .IsRequired();

            builder
                .Property(sr => sr.ReservedAtUtc)
                .IsRequired();

            builder
                .HasOne(sr => sr.Seat)
                .WithMany(s => s.SeatReservations)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(sr => sr.MovieSession)
                .WithMany(ms => ms.SeatReservations)
                .IsRequired();
        }
    }
}
