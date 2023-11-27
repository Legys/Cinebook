using Cinebook.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Movies.Command;

public record DeleteMovieCommand(Guid MovieId) : IRequest;

public class DeleteMovieCommandHandler(AppDbContext context) : IRequestHandler<DeleteMovieCommand>
{
    public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        await context.Movies.Where(m => m.MovieId == request.MovieId).ExecuteDeleteAsync(cancellationToken);
    }
}