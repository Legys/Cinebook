namespace Cinebook.Application.Common.Model.Pagination;

public class PagedResponse<T>
{
    public required List<T> Data { get; init; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}