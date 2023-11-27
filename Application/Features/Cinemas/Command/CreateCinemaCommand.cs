using Cinebook.Application.Features.Cinemas.Model.Request;
using Cinebook.Application.Features.Cinemas.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Persistence;
using MediatR;

namespace Cinebook.Application.Features.Cinemas.Command;

public record CreateCinemaCommand(CreateCinemaRequest Request) : IRequest<CinemaResponse>;

public class CreateCinemaCommandHandler(AppDbContext context) : IRequestHandler<CreateCinemaCommand, CinemaResponse>
{
    public async Task<CinemaResponse> Handle(CreateCinemaCommand request, CancellationToken cancellationToken)
    {
        var newCinema = new Cinema
        {
            Name = request.Request.Name,
            Address = request.Request.Address,
            BaseTicketPrice = request.Request.BaseTicketPrice,
            OpeningAtUtc = request.Request.OpeningAtUtc,
            ClosingAtUtc = request.Request.ClosingAtUtc,
            MovieSessions = [],
            Seats = []
        };

        await context.Cinemas.AddAsync(newCinema, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return CinemaResponse.FromDomain(newCinema);
    }
}