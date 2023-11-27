using Cinebook.Application.Features.Genres.Model.Response;
using Cinebook.Domain.Entities;

namespace Cinebook.Application.Features.Movies.Model.Response;

public class MovieResponse
{
    public Guid MovieId { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required DateTime ReleaseDateUtc { get; init; }
    public required List<GenreResponse> Genres { get; init; } = [];
    public static MovieResponse FromDomain(Movie movie)

    {
        return new MovieResponse
        {
            MovieId = movie.MovieId,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDateUtc = movie.ReleaseDateUtc,
            Genres = movie.Genres.ConvertAll(GenreResponse.FromDomain)
        };
    }
}