namespace Cinebook.Domain.Entities;

public class Genre
{
    public Guid GenreId { get; set; }
    public required string Name { get; set; }
    public List<Movie> Movies { get; set; } = [];
}