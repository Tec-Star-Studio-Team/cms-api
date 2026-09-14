using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Projects.DTOs;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetPaginatedProjects;

public sealed record GetPaginatedProjectsQuery(int LastId, int PageSize) : IQuery<OffSetPagedResult<ProjectDto>>;
