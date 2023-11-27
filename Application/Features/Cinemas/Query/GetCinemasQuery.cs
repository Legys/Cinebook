using Cinebook.Application.Common.Model.Pagination;
using Cinebook.Application.Extensions;
using Cinebook.Application.Features.Cinemas.Model.Response;
using Cinebook.Infrastructure.Persistence;
using MediatR;

namespace Cinebook.Application.Features.Cinemas.Query;

public record GetCinemasQuery(PagedRequest PagedRequest) : IRequest<PagedResponse<CinemaResponse>>;

public class GetCinemasQueryHandler(AppDbContext context)
    : IRequestHandler<GetCinemasQuery, PagedResponse<CinemaResponse>>
{
    public async Task<PagedResponse<CinemaResponse>> Handle(GetCinemasQuery request,
        CancellationToken cancellationToken)
    {
        var cinemas = await context.Cinemas
            .Select(x => CinemaResponse.FromDomain(x))
            .ToPagedResponseAsync(request.PagedRequest, cancellationToken);

        return cinemas;
    }
}