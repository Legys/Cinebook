using Cinebook.Application.Features.Tickets.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.Tickets;
public class TicketsController(ISender sender) : ControllerBase
{
    [HttpGet("api/tickets")]
    public async Task<IActionResult> GetTicketsByEmail(string customerEmail)
    {
        var tickets = await sender.Send(new GetTicketsQuery(customerEmail));
        return Ok(tickets);
    }
}
