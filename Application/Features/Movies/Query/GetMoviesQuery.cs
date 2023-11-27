using Cinebook.Application.Common.Model.Pagination;
using Cinebook.Application.Extensions;
using Cinebook.Application.Features.Movies.Model.Response;
using Cinebook.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Movies.Query;

public record GetMoviesQuery : IRequest<PagedResponse<MovieResponse>>
{
    public required PagedRequest PagedRequest { get; init; }
}

public class GetMoviesQueryHandler(AppDbContext context) : IRequestHandler<GetMoviesQuery, PagedResponse<MovieResponse>>
{
    public async Task<PagedResponse<MovieResponse>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await context.Movies
            .Include(m => m.Genres)
            .Select(m => MovieResponse.FromDomain(m))
            .ToPagedResponseAsync(request.PagedRequest, cancellationToken);

        return movies;
    }
}