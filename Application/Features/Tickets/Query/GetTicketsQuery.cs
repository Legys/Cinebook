using Cinebook.Application.Features.Tickets.Model.Response;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Tickets.Query;
public record GetTicketsQuery(string CustomerEmail) : IRequest<List<TicketResponse>>;

public class GetTicketsQueryHandler(AppDbContext context) : IRequestHandler<GetTicketsQuery, List<TicketResponse>>
{
    public async Task<List<TicketResponse>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        var bookedSeats = await context.SeatReservations
            .Where(sr => sr.CustomerEmail == request.CustomerEmail)
            .Where(sr => sr.IsBooked)
            .Include(sr => sr.Seat)
            .Include(sr => sr.MovieSession)
            .ThenInclude(ms => ms!.Movie)
            .Include(sr => sr.MovieSession)
            .ThenInclude(ms => ms!.Cinema)
            .ToListAsync(cancellationToken);

        if (bookedSeats == null)
        {
            throw new NotFoundException(ApplicationErrors.TicketsNotFound);
        }

        if (bookedSeats.Count == 0)
        {
            return [];
        }

        var movieSession = bookedSeats[0].MovieSession;

        if (movieSession is null)
        {
            throw new InternalServerException(ApplicationErrors.InternalServerError_Message);
        }

        var movie = movieSession.Movie;
        var cinema = movieSession.Cinema;

        return bookedSeats.ConvertAll(sr =>
        {
            if (sr.Seat is null) throw new InternalServerException(ApplicationErrors.InternalServerError_Message);

            return new TicketResponse
            {
                MovieTitle = movie.Title,
                CinemaName = cinema.Name,
                StartTimeUtc = movieSession.StartTimeUtc,
                DurationInMinutes = movie.DurationInMinutes,
                SeatNumber = sr.Seat.Column,
                SeatRow = sr.Seat.Row,
                CustomerEmail = sr.CustomerEmail,
            };
        });
    }
}
