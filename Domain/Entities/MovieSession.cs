namespace Cinebook.Domain.Entities;

public class MovieSession
{
    public Guid MovieSessionId { get; set; }
    public required Movie Movie { get; set; }
    public required Cinema Cinema { get; set; }
    public required DateTime StartTimeUtc { get; set; }
    public required DateTime EndTimeUtc { get; set; }
    public required List<SeatReservation> SeatReservations { get; set; }
}