using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Apps.DTOs;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Infrastructure.Persistence;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Server.Application.Features.Apps.Queries.GetAppById;

public class GetAppByIdHandler(AppDbContext context) : IQueryHandler<GetAppByIdQuery, Result<AppDto>>
{
    public async ValueTask<Result<AppDto>> Handle(GetAppByIdQuery query, CancellationToken cancellationToken)
    {
        var app = await context.Apps
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == query.AppId, cancellationToken);

        if (app is null)
            return Result<AppDto>.NotFound(string.Format(AppErrors.General.NotFound, query.AppId));

        return Result<AppDto>.Success(new AppDto(
            Id: app.Id,
            Name: app.Name,
            ProjectId: app.ProjectId,
            LanguageId: app.LanguageId,
            TemplateId: app.TemplateId
        ));
    }
}
