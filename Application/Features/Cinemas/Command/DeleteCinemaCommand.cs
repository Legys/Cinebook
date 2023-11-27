using Cinebook.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Cinemas.Command;

public record DeleteCinemaCommand(Guid CinemaId) : IRequest;

public class DeleteCinemaCommandHandler(AppDbContext context) : IRequestHandler<DeleteCinemaCommand>
{
    public async Task Handle(DeleteCinemaCommand request, CancellationToken cancellationToken)
    {
        await context.Cinemas.Where(c => c.CinemaId == request.CinemaId).ExecuteDeleteAsync(cancellationToken);
    }
}