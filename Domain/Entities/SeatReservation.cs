using System.ComponentModel.DataAnnotations;

namespace Cinebook.Domain.Entities;
public class SeatReservation
{
    public Guid SeatReservationId { get; set; }
    public required bool IsBooked { get; set; }
    public required string CustomerEmail { get; set; }
    public required DateTime ReservedAtUtc { get; set; }
    public required Guid SeatId { get; set; }
    [Required]
    public Seat? Seat { get; set; }
    public required Guid MovieSessionId { get; set; }
    [Required]
    public MovieSession? MovieSession { get; set; }
}
