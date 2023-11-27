using Cinebook.Application.Features.MovieSessions.Command;
using Cinebook.Application.Features.MovieSessions.Model.Request;
using Cinebook.Application.Features.MovieSessions.Query;
using Cinebook.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.MovieSessions;

public class MovieSessionsController(ISender sender) : ControllerBase
{
    [HttpGet("api/cinemas/{cinemaId:guid}/sessions")]
    public async Task<IActionResult> GetCinemaSessions(Guid cinemaId)
    {
        var result = await sender.Send(new GetCinemaSessions(cinemaId));
        return Ok(result);
    }

    [HttpPost("/api/cinemas/{cinemaId:guid}/sessions/schedule")]
    public async Task<ActionResult<MovieSession>> ScheduleMovieSession(Guid cinemaId, [FromBody] ScheduleMovieSessionsRequest request)
    {
        await sender.Send(new ScheduleMovieSessionsCommand(cinemaId, request.StartDate, request.DaysAhead));

        return CreatedAtAction(nameof(GetCinemaSessions), new { cinemaId }, null);
    }
}
