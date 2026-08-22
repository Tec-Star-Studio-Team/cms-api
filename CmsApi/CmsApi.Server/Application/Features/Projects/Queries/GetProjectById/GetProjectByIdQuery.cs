using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.DTOs;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetProjectById;

public sealed record GetProjectByIdQuery(int ProjectId) : IQuery<Result<ProjectResponseDto>>;
