using Cinebook.Application.Common.Model.Pagination;
using Cinebook.Application.Features.Movies.Command;
using Cinebook.Application.Features.Movies.Model.Request;
using Cinebook.Application.Features.Movies.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.Movies;

[ApiController]
public class MoviesController(ISender sender) : Controller
{
    [HttpGet("/api/movies")]
    public async Task<IActionResult> GetAllMovies([FromQuery] MoviesRequest request)
    {
        var query = new GetMoviesQuery
        {
            PagedRequest = new PagedRequest(request.PageNumber, request.PageSize)
        };
        var result = await sender.Send(query);

        return Ok(result);
    }

    [HttpPost("/api/movies")]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request)
    {
        var command = new CreateMovieCommand(request);
        var result = await sender.Send(command);

        return Ok(result);
    }

    [HttpPut("/api/movies/{movieId:guid}")]
    public async Task<IActionResult> UpdateMovie(Guid movieId, [FromBody] UpdateMovieRequest request)
    {
        var command = new UpdateMovieCommand(movieId, request);
        var result = await sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("/api/movies/{movieId:guid}")]
    public async Task<IActionResult> DeleteMovie(Guid movieId)
    {
        var command = new DeleteMovieCommand(movieId);
        await sender.Send(command);

        return NoContent();
    }
}