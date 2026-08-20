namespace Crm.Application.Common.Models;

public sealed record PaginatedResult<T>(
IReadOnlyList<T> Items,
    int PageNumber,
    int PageSize,
    int TotalCount)
{
    public int TotalPages =>(int)Math.Ceiling(TotalCount / (double)PageSize);
}