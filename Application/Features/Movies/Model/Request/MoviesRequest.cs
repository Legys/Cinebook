using Cinebook.Application.Common.Model.Pagination;

namespace Cinebook.Application.Features.Movies.Model.Request;

public record MoviesRequest(int PageNumber, int PageSize) : PagedRequest(PageNumber, PageSize);