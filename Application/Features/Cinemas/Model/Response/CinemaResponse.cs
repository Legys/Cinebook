using Cinebook.Domain.Entities;

namespace Cinebook.Application.Features.Cinemas.Model.Response;

public class CinemaResponse
{
    public required Guid CinemaId { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }

    public static CinemaResponse FromDomain(Cinema cinema)
    {
        return new CinemaResponse
        {
            CinemaId = cinema.CinemaId,
            Name = cinema.Name,
            Address = cinema.Address
        };
    }
}