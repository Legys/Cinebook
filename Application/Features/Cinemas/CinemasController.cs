using Cinebook.Application.Common.Model.Pagination;
using Cinebook.Application.Features.Cinemas.Command;
using Cinebook.Application.Features.Cinemas.Model.Request;
using Cinebook.Application.Features.Cinemas.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.Cinemas;

[ApiController]
public class CinemasController(ISender sender) : ControllerBase
{
    [HttpGet("api/cinemas")]
    public async Task<IActionResult> GetCinemas([FromQuery] CinemasRequest request)
    {
        var query = new GetCinemasQuery(new PagedRequest(request.PageNumber, request.PageSize));
        var result = await sender.Send(query);
        return Ok(result);
    }

    [HttpGet("api/cinemas/{cinemaId:guid}")]
    public async Task<IActionResult> GetCinema(Guid cinemaId)
    {
        var result = await sender.Send(new GetCinemaByIdQuery(cinemaId));
        return Ok(result);
    }

    [HttpPost("api/cinemas")]
    public async Task<IActionResult> CreateCinema([FromBody] CreateCinemaRequest request)
    {
        var result = await sender.Send(new CreateCinemaCommand(request));
        return CreatedAtAction(nameof(GetCinema), new { cinemaId = result.CinemaId }, result);
    }

    [HttpPut("api/cinemas/{cinemaId:guid}")]
    public async Task<IActionResult> UpdateCinema(Guid cinemaId, UpdateCinemaRequest request)
    {
        var result = await sender.Send(new UpdateCinemaCommand(cinemaId, request));
        return Ok(result);
    }

    [HttpDelete("api/cinemas/{cinemaId:guid}")]
    public async Task<IActionResult> DeleteCinema(Guid cinemaId)
    {
        await sender.Send(new DeleteCinemaCommand(cinemaId));
        return NoContent();
    }
}