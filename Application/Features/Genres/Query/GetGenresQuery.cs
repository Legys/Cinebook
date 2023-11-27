using Cinebook.Application.Features.Genres.Model.Response;
using Cinebook.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Genres.Query;

public record GetGenresQuery : IRequest<List<GenreResponse>>;

public class GetGenresQueryHandler(AppDbContext context) : IRequestHandler<GetGenresQuery, List<GenreResponse>>
{
    public async Task<List<GenreResponse>> Handle(GetGenresQuery _, CancellationToken cancellationToken)
    {
        var genres = await context.Genres
            .ToListAsync(cancellationToken);

        return genres
            .ConvertAll(GenreResponse.FromDomain)
;
    }
}
