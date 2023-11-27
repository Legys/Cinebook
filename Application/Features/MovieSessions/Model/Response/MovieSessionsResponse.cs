using Cinebook.Application.Features.Movies.Model.Response;
using Cinebook.Domain.Entities;

namespace Cinebook.Application.Features.MovieSessions.Model.Response;
public record MovieSessionResponse
{
    public required Guid MovieSessionId { get; set; }
    public required DateTime StartTimeUtc { get; set; }
    public required DateTime EndTimeUtc { get; set; }
    public required MovieResponse Movie { get; set; }

    public static MovieSessionResponse FromDomain(MovieSession movieSession)
    {
        return new MovieSessionResponse
        {
            MovieSessionId = movieSession.MovieSessionId,
            StartTimeUtc = movieSession.StartTimeUtc,
            EndTimeUtc = movieSession.EndTimeUtc,
            Movie = MovieResponse.FromDomain(movieSession.Movie)
        };
    }
}