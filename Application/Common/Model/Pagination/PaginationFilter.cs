namespace Cinebook.Application.Common.Model.Pagination;

public record PaginationFilter
{
    public PaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize <= 0 ? 10 : pageSize;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
}