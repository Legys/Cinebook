using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder
            .ToTable("Movies")
            .HasKey(m => m.MovieId);

        builder
            .Property(m => m.MovieId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(m => m.Description)
            .HasMaxLength(2000);

        builder
            .Property(m => m.ReleaseDateUtc)
            .IsRequired();

        builder
            .Property(m => m.DurationInMinutes)
            .IsRequired();

        builder
            .HasMany<Genre>(m => m.Genres)
            .WithMany(g => g.Movies);
    }
}