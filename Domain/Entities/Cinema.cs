namespace Cinebook.Domain.Entities;

public class Cinema
{
    public Guid CinemaId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required DateTime OpeningAtUtc { get; set; }
    public required DateTime ClosingAtUtc { get; set; }
    public required int BaseTicketPrice { get; set; }
    public required List<MovieSession> MovieSessions { get; set; }
    public required List<Seat> Seats { get; set; }
}