using Cinebook.Application.Features.Movies.Model.Request;
using Cinebook.Application.Features.Movies.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Movies.Command;

public record CreateMovieCommand(CreateMovieRequest CreateMovieRequest) : IRequest<MovieResponse>;

public class CreateMovieHandler(AppDbContext context) : IRequestHandler<CreateMovieCommand, MovieResponse>
{
    public async Task<MovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var createMovieRequest = request.CreateMovieRequest;
        var genres = await context.Genres
            .Where(g => createMovieRequest.Genres.Contains(g.GenreId))
            .ToListAsync(cancellationToken);

        if (genres.Count != createMovieRequest.Genres.Count)
            throw new NotFoundException(ApplicationErrors.GenresNotFound_Message);

        var newMovie = new Movie
        {
            Title = createMovieRequest.Title,
            Description = createMovieRequest.Description,
            ReleaseDateUtc = createMovieRequest.ReleaseDateUtc,
            DurationInMinutes = createMovieRequest.DurationInMinutes,
            Genres = genres
        };

        await context.Movies.AddAsync(newMovie, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return MovieResponse.FromDomain(newMovie);
    }
}