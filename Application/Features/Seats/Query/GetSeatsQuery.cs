using Cinebook.Application.Features.Seats.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Domain.Rules.TickerPrice;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Seats.Query;
public record GetSeatsQuery(Guid MovieSessionId) : IRequest<List<SeatResponse>>;

public class GetSeatsQueryHandler(AppDbContext context) : IRequestHandler<GetSeatsQuery, List<SeatResponse>>
{
    public async Task<List<SeatResponse>> Handle(GetSeatsQuery request, CancellationToken cancellationToken)
    {
        var movieSession = await context.MovieSessions
            .Where(ms => ms.MovieSessionId == request.MovieSessionId)
            .Include(ms => ms.Cinema)
            .ThenInclude(c => c.Seats
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Column))
            .FirstOrDefaultAsync(cancellationToken);

        if (movieSession is null)
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, nameof(MovieSession)));

        var cinema = movieSession.Cinema;
        var seats = cinema.Seats;

        var seatReservations = await context.SeatReservations
            .Where(sr => sr.MovieSessionId == request.MovieSessionId)
            .ToListAsync(cancellationToken);

        return seats.ConvertAll(s =>
        {
            var seatReservation = seatReservations.Find(sr => sr.SeatId == s.SeatId);
            var priceCents = TicketPrice.GetTicketPrice(cinema.BaseTicketPrice, movieSession.StartTimeUtc, s.SeatType);

            return new SeatResponse
            {
                SeatId = s.SeatId,
                Row = s.Row,
                Column = s.Column,
                IsReserved = seatReservation != null,
                IsBooked = seatReservation?.IsBooked ?? false,
                PriceCents = priceCents,
                SeatType = s.SeatType
            };
        });
    }
}