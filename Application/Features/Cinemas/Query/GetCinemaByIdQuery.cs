using Cinebook.Application.Features.Cinemas.Model.Response;
using Cinebook.Domain.Entities;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Features.Cinemas.Query;

public record GetCinemaByIdQuery(Guid CinemaId) : IRequest<CinemaResponse>;

public class GetCinemaByIdQueryHandler(AppDbContext context) : IRequestHandler<GetCinemaByIdQuery, CinemaResponse>
{
    public async Task<CinemaResponse> Handle(GetCinemaByIdQuery request, CancellationToken cancellationToken)
    {
        var cinema = await context.Cinemas
            .FirstOrDefaultAsync(x => x.CinemaId == request.CinemaId, cancellationToken);

        if (cinema is null)
            throw new NotFoundException(string.Format(ApplicationErrors.NotFoundError_Message, typeof(Cinema)));

        return CinemaResponse.FromDomain(cinema);
    }
}