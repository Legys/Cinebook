using Cinebook.Domain.Rules;
using Cinebook.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Seats.Command;
public record ReleaseReservedSeatsCommand : IRequest;

public class ReleaseReservedSeatsCommandHandler(AppDbContext context) : IRequestHandler<ReleaseReservedSeatsCommand>
{
    private readonly TimeSpan reservationTimeout = TimeSpan.FromMinutes(CinemaConsts.SeatReservationInMinutes);

    public async Task Handle(ReleaseReservedSeatsCommand request, CancellationToken cancellationToken)
    {
        var expirationTimeUtc = DateTime.UtcNow.Subtract(reservationTimeout);
        var expiredReservations = await context.SeatReservations
                .Where(sr => !sr.IsBooked && expirationTimeUtc > sr.ReservedAtUtc)
                .ToListAsync(cancellationToken);

        if (expiredReservations.Count > 0)
        {
            context.RemoveRange(expiredReservations);
            await context.SaveChangesAsync(cancellationToken);
        }

        return;
    }
}
