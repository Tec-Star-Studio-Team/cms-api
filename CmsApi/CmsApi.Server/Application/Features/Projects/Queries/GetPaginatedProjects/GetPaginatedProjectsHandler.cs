using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Projects.DTOs;
using CmsApi.Server.Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetPaginatedProjects;

public sealed class GetPaginatedProjectsHandler(AppDbContext appDbContext) : IQueryHandler<GetPaginatedProjectsQuery, OffSetPagedResult<ProjectDto>>
{
    public async ValueTask<OffSetPagedResult<ProjectDto>> Handle(GetPaginatedProjectsQuery query, CancellationToken cancellationToken)
    {
        var baseQuery = appDbContext
            .Projects
            .AsNoTracking()
            .OrderBy(p => p.Id);

        var countTask = await baseQuery.CountAsync(cancellationToken);

        var itemsTask = await baseQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProjectDto(p.Id, p.Name, p.Description))
            .ToListAsync(cancellationToken);

        var totalCount = countTask;
        var items = itemsTask;
        var totalPages = (int)Math.Ceiling(totalCount / (double)query.PageSize);

        return new OffSetPagedResult<ProjectDto>(
            items: items,
            Page: query.Page,
            PageSize: query.PageSize,
            TotalCount: totalCount,
            TotalPages: totalPages,
            HasNextPage: query.Page < totalPages,
            HasPreviousPage: query.Page > 1
        );
    }
}
