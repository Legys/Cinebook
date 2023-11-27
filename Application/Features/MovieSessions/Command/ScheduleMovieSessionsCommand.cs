using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.MovieSessions.Command;
public record ScheduleMovieSessionsCommand(Guid CinemaId, DateTime StartDateUtc, int DaysAhead) : IRequest;

public class ScheduleMovieSessionsCommandHandler(AppDbContext context, MovieSessionsFactory movieSessionsFactory) : IRequestHandler<ScheduleMovieSessionsCommand>
{
    public async Task Handle(ScheduleMovieSessionsCommand request, CancellationToken cancellationToken)
    {
        var startDateDayUtc = new DateTime(request.StartDateUtc.Year, request.StartDateUtc.Month, request.StartDateUtc.Day).ToUniversalTime();

        var cinema = await context.Cinemas
            .Include(c => c.MovieSessions
              .Where(ms => ms.StartTimeUtc >= startDateDayUtc)
            )
            .FirstOrDefaultAsync(c => c.CinemaId == request.CinemaId, cancellationToken);

        if (cinema is null)
        {
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, typeof(Cinema)));
        }

        if (cinema.MovieSessions.Count > 0)
        {
            throw new AlreadyExistsException(string.Format(ApplicationErrors.AlreadyExistsError_Message, typeof(MovieSession), request.StartDateUtc));
        }

        var movies = await context.Movies.ToListAsync(cancellationToken);
        var movieSessions = movieSessionsFactory.Init(movies, cinema).CreateMovieSessions(request.StartDateUtc, request.DaysAhead);

        context.MovieSessions.AddRange(movieSessions);
        await context.SaveChangesAsync(cancellationToken);

        return;
    }
}