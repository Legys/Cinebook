using Cinebook.Application.Features.MovieSessions.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.MovieSessions.Query;
public record GetCinemaSessions(Guid CinemaId) : IRequest<CinemaSessionsResponse>;

public class GetCinemaSessionsQueryHandler(AppDbContext context) : IRequestHandler<GetCinemaSessions, CinemaSessionsResponse>
{
    public async Task<CinemaSessionsResponse> Handle(GetCinemaSessions request, CancellationToken cancellationToken)
    {
        var cinemas = await context.Cinemas
            .Include(c => c.MovieSessions
                .Where(ms => ms.StartTimeUtc >= DateTime.UtcNow)
                .OrderBy(ms => ms.StartTimeUtc)
                )
            .ThenInclude(ms => ms.Movie)
            .ThenInclude(m => m.Genres)
            .Where(c => c.CinemaId == request.CinemaId)
            .FirstOrDefaultAsync(cancellationToken);

        if (cinemas is null)
        {
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, typeof(Cinema)));
        }

        return CinemaSessionsResponse.FromDomain(cinemas);
    }
}
