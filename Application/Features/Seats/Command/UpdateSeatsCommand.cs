using Cinebook.Application.Features.Cinemas.Model.Response;
using Cinebook.Application.Features.Seats.Model.Request;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Seats.Command;

public record UpdateSeatsCommand(Guid CinemaId, UpdateSeatsRequest Request) : IRequest<CinemaResponse>;

public class UpdateSeatsCommandHandler(AppDbContext context, SeatsFactory seatsFactory) : IRequestHandler<UpdateSeatsCommand, CinemaResponse>
{
    public async Task<CinemaResponse> Handle(UpdateSeatsCommand request, CancellationToken cancellationToken)
    {
        var cinema = await context.Cinemas
            .Include(c => c.Seats)
            .FirstOrDefaultAsync(c => c.CinemaId == request.CinemaId, cancellationToken);

        if (cinema is null)
        {
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, nameof(Cinema)));
        }

        var newSeatsArrangement = seatsFactory.Init(cinema).CreateSeats(request.Request.SeatsConfig);

        context.Seats.AddRange(newSeatsArrangement);
        cinema.Seats = newSeatsArrangement;

        await context.SaveChangesAsync(cancellationToken);

        return CinemaResponse.FromDomain(cinema);
    }
}
