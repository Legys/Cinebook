using Cinebook.Domain.Entities;
using Cinebook.Domain.Enums;

namespace Cinebook.Application.Features.Seats;

public record SeatConfig(int Row, int RowSize, SeatTypeEnum RowType);

public class SeatsFactory()
{
    // Backing field for Cinema
    private Cinema? CinemaInit;
    private Cinema Cinema
    {
        get
        {
            if (CinemaInit is null)
            {
                throw new ArgumentNullException(nameof(Cinema));
            }
            return CinemaInit;
        }
    }

    public SeatsFactory Init(Cinema cinema)
    {
        CinemaInit = cinema;
        return this;
    }

    public List<Seat> CreateSeats(List<SeatConfig> seatsConfig)
    {
        var seats = seatsConfig
            .SelectMany(config =>
                Enumerable.Range(0, config.RowSize)
                    .Select(index => CreateSeat(config.Row, index, config.RowType))
            ).ToList();

        return seats;
    }

    private Seat CreateSeat(int row, int column, SeatTypeEnum seatType)
    {
        return new Seat
        {
            Row = row,
            Column = column,
            Cinema = Cinema,
            SeatType = seatType,
            SeatReservations = []
        };
    }
}