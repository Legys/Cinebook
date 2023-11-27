using Cinebook.Application.Features.Cinemas.Model.Request;
using Cinebook.Application.Features.Cinemas.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Cinemas.Command;

public record UpdateCinemaCommand(Guid CinemaId, UpdateCinemaRequest Request) : IRequest<CinemaResponse>;

public class UpdateCinemaCommandHandler(AppDbContext context) : IRequestHandler<UpdateCinemaCommand, CinemaResponse>
{
    public async Task<CinemaResponse> Handle(UpdateCinemaCommand request, CancellationToken cancellationToken)
    {
        var cinema = await context.Cinemas.FirstOrDefaultAsync(c => c.CinemaId == request.CinemaId, cancellationToken);

        if (cinema is null)
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, nameof(Cinema)));

        cinema.Name = request.Request.Name;
        cinema.Address = request.Request.Address;
        cinema.BaseTicketPrice = request.Request.BaseTicketPrice;

        await context.SaveChangesAsync(cancellationToken);

        return CinemaResponse.FromDomain(cinema);
    }
}