using Cinebook.Application.Features.Seats.Model.Request;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Seats.Command;
public record BookSeatsCommand(BookSeatsRequest Request) : IRequest;

public class BookSeatsCommandHandler(AppDbContext context) : IRequestHandler<BookSeatsCommand>
{
    public async Task Handle(BookSeatsCommand request, CancellationToken cancellationToken)
    {
        var seatReservations = await context.SeatReservations
             .Where(sr => request.Request.ReservationIds.Contains(sr.SeatReservationId))
             .Where(sr => !sr.IsBooked)
             .ToListAsync(cancellationToken);

        if (seatReservations.Count != request.Request.ReservationIds.Count)
        {
            throw new AlreadyExistsException(ApplicationErrors.SeatsAlreadyBooked);
        }

        seatReservations.ForEach(sr => sr.IsBooked = true);

        await context.SaveChangesAsync(cancellationToken);

        return;
    }
}
