using Cinebook.Application.Features.Booking.Command;
using Cinebook.Application.Features.Seats.Command;
using Cinebook.Application.Features.Seats.Model.Request;
using Cinebook.Application.Features.Seats.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.Seats;

[ApiController]
public class SeatsController(ISender sender) : ControllerBase
{
    [HttpGet("api/sessions/{sessionId:guid}/seats")]
    public async Task<IActionResult> GetSeats(Guid sessionId)
    {
        var query = new GetSeatsQuery(sessionId);
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpPost("api/sessions/{sessionId:guid}/seats/reserve")]
    public async Task<IActionResult> ReserveSeats(Guid sessionId, [FromBody] ReserveSeatsRequest request)
    {
        var result = await sender.Send(new ReserveSeatsCommand(sessionId, request));

        return Ok(result);
    }

    [HttpPost("api/seats/book")]
    public async Task<IActionResult> BookSeats([FromBody] BookSeatsRequest request)
    {
        await sender.Send(new BookSeatsCommand(request));

        return NoContent();
    }

    [HttpPut("api/cinemas/{cinemaId:guid}/seats")]
    public async Task<IActionResult> UpdateSeats(Guid cinemaId, [FromBody] UpdateSeatsRequest request)
    {
        var command = new UpdateSeatsCommand(cinemaId, request);
        var result = await sender.Send(command);

        return Ok(result);
    }
}
