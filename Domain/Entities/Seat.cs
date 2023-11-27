using Cinebook.Domain.Enums;

namespace Cinebook.Domain.Entities;

public class Seat
{
    public Guid SeatId { get; set; }
    public required Cinema Cinema { get; set; }
    public required int Row { get; set; }
    public required int Column { get; set; }
    public required SeatTypeEnum SeatType { get; set; }
    public required List<SeatReservation> SeatReservations { get; set; }
}