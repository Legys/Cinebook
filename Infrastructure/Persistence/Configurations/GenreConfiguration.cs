using Cinebook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinebook.Infrastructure.Persistence.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .ToTable("Genres")
            .HasKey(g => g.GenreId);

        builder
            .Property(g => g.GenreId)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .HasMany<Movie>(g => g.Movies)
            .WithMany(m => m.Genres);
    }
}