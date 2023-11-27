using Cinebook.Application.Features.Genres.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinebook.Application.Features.Genres;

[ApiController]
public class GenresController(ISender sender) : ControllerBase
{
    [HttpGet("/api/genres")]
    public async Task<IActionResult> GetGenres()
    {
        var result = await sender.Send(new GetGenresQuery());

        return Ok(result);
    }
}
