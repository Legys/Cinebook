namespace Cinebook.Application.Features.Seats.Model.Request;
public record ReserveSeatsRequest(List<Guid> SeatsIds, string CustomerEmail);
