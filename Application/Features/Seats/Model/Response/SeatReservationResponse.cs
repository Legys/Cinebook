namespace Cinebook.Application.Features.Seats.Model.Response;
public class SeatReservationsResponse
{
    public required List<Guid> SeatReservationIds { get; set; }
    public required string CustomerEmail { get; set; }
}
