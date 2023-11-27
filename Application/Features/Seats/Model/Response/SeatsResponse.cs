using Cinebook.Domain.Enums;

namespace Cinebook.Application.Features.Seats.Model.Response;

public record SeatResponse
{
    public required Guid SeatId { get; set; }
    public required int Row { get; set; }
    public required int Column { get; set; }
    public required bool IsReserved { get; set; }
    public required bool IsBooked { get; set; }
    public required double PriceCents { get; set; }
    public required SeatTypeEnum SeatType { get; set; }
}
