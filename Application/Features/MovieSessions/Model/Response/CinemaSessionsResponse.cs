using Cinebook.Domain.Entities;

namespace Cinebook.Application.Features.MovieSessions.Model.Response;

public class CinemaSessionsResponse
{
    public Guid CinemaId { get; set; }
    public required string Name { get; set; }

    public required string Address { get; set; }
    public required List<MovieSessionResponse> MovieSessions { get; set; }

    public static CinemaSessionsResponse FromDomain(Cinema cinema)
    {
        return new CinemaSessionsResponse
        {
            CinemaId = cinema.CinemaId,
            Name = cinema.Name,
            Address = cinema.Address,
            MovieSessions = cinema.MovieSessions.ConvertAll(MovieSessionResponse.FromDomain)
        };
    }
}
