using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.DTOs;
using CmsApi.Server.Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdHandler(AppDbContext appDbContext) : IQueryHandler<GetProjectByIdQuery, Result<ProjectResponseDto>>
{
    public async ValueTask<Result<ProjectResponseDto>> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == query.ProjectId, cancellationToken);

        if (project is null)
            return Result<ProjectResponseDto>.NotFound($"Project {query.ProjectId} not found.");

        return Result<ProjectResponseDto>.Success(new ProjectResponseDto(

            Id: project.Id,
            Name: project.Name,
            Description: project.Description,
            CreatedAt: project.CreatedAt,
            UpdatedAt: project.UpdatedAt
        ));
    }
}
