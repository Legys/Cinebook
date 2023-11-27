using Cinebook.Application.Features.Movies.Model.Request;
using Cinebook.Application.Features.Movies.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Movies.Command;

public record UpdateMovieCommand(Guid MovieId, UpdateMovieRequest UpdateMovieRequest) : IRequest<MovieResponse>;

public class UpdateMovieCommandHandler(AppDbContext context) : IRequestHandler<UpdateMovieCommand, MovieResponse>
{
    public async Task<MovieResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var updateMovieRequest = request.UpdateMovieRequest;

        var movie = await context.Movies
            .Include(m => m.Genres)
            .FirstOrDefaultAsync(m => m.MovieId == request.MovieId, cancellationToken);

        var newGenres = await context.Genres
            .Where(g => updateMovieRequest.Genres.Contains(g.GenreId))
            .ToListAsync(cancellationToken);

        if (movie is null)
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, nameof(Movie)));

        if (newGenres.Count != updateMovieRequest.Genres.Count)
            throw new NotFoundException(ApplicationErrors.GenresNotFound_Message);

        movie.Update(
            updateMovieRequest.Title,
            updateMovieRequest.Description,
            updateMovieRequest.ReleaseDate,
            updateMovieRequest.DurationInMinutes,
            newGenres);

        await context.SaveChangesAsync(cancellationToken);

        return MovieResponse.FromDomain(movie);
    }
}