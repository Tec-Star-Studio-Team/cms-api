namespace CmsApi.Server.Application.Common.Models;

public sealed record OffSetPagedResult<T>(
    IReadOnlyList<T> items,
    int LastId
);
