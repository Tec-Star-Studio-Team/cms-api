namespace CmsApi.Server.Application.Common.Models;

public sealed record OffSetPagedResult<T>(
    IReadOnlyList<T> items,
    int Page,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage
);
