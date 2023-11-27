namespace Cinebook.Application.Features.Tickets.Model.Response;
public class TicketResponse
{
    public required string CustomerEmail { get; set; }
    public required string MovieTitle { get; set; }
    public required DateTime StartTimeUtc { get; set; }
    public required int DurationInMinutes { get; set; }
    public required string CinemaName { get; set; }
    public required int SeatNumber { get; set; }
    public required int SeatRow { get; set; }
}
