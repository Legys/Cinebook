using Cinebook.Domain.Entities;

namespace Cinebook.Application.Features.Genres.Model.Response;

public class GenreResponse
{
    public Guid GenreId { get; init; }
    public required string Name { get; init; }

    public static GenreResponse FromDomain(Genre genre)
    {
        return new GenreResponse
        {
            GenreId = genre.GenreId,
            Name = genre.Name
        };
    }
}