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
        int lastId = query.LastId;

        var baseQuery = appDbContext
            .Projects
            .Where(p => lastId == 0 || p.Id > lastId)
            .OrderBy(p => p.Id)
            .Take(query.PageSize)
            .Select(p => new ProjectDto(p.Id, p.Name, p.Description))
            .AsNoTracking();

        // ToQueryString(): Used for debugging only
        var queryString = baseQuery.ToQueryString();

        var items = await baseQuery.ToListAsync(cancellationToken);

        return new OffSetPagedResult<ProjectDto>(
            items: items,
            LastId: items.LastOrDefault()?.Id ?? 0
        );
    }
}
