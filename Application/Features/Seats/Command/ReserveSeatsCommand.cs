using Cinebook.Application.Features.Seats.Model.Request;
using Cinebook.Application.Features.Seats.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Persistence;
using MediatR;

namespace Cinebook.Application.Features.Booking.Command;
public record ReserveSeatsCommand(Guid MovieSessionId, ReserveSeatsRequest ReserveSeatsRequest) : IRequest<SeatReservationsResponse>;

public class ReserveSeatsCommandHandler(AppDbContext context) : IRequestHandler<ReserveSeatsCommand, SeatReservationsResponse>
{
    public async Task<SeatReservationsResponse> Handle(ReserveSeatsCommand request, CancellationToken cancellationToken)
    {
        var reservations = request.ReserveSeatsRequest.SeatsIds.ConvertAll(s => new SeatReservation
        {
            SeatId = s,
            MovieSessionId = request.MovieSessionId,
            CustomerEmail = request.ReserveSeatsRequest.CustomerEmail,
            ReservedAtUtc = DateTime.UtcNow,
            IsBooked = false,
        });

        await context.SeatReservations.AddRangeAsync(reservations, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new SeatReservationsResponse
        {
            CustomerEmail = request.ReserveSeatsRequest.CustomerEmail,
            SeatReservationIds = reservations.ConvertAll(r => r.SeatReservationId)
        };
    }
}
