using Cinebook.Application.Common.Model.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Cinebook.Application.Extensions;

public static class QueryableExtension
{
    public static async Task<PagedResponse<T>> ToPagedResponseAsync<T>(this IQueryable<T> queryable,
        PagedRequest pagedRequest, CancellationToken cancellationToken)
    {
        var validFilter = new PaginationFilter(pagedRequest.PageNumber, pagedRequest.PageSize);
        var totalRecords = await queryable.CountAsync(cancellationToken);
        var pagedData = await queryable.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize).ToListAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((decimal)totalRecords / validFilter.PageSize);
        var hasNextPage = validFilter.PageNumber < totalPages;
        var hasPreviousPage = validFilter.PageNumber > 1 && validFilter.PageNumber <= totalPages;

        return new PagedResponse<T>
        {
            Data = pagedData,
            PageNumber = validFilter.PageNumber,
            PageSize = validFilter.PageSize,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            HasNextPage = hasNextPage,
            HasPreviousPage = hasPreviousPage
        };
    }
}