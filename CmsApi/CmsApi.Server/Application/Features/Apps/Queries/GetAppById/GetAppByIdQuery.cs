using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Apps.DTOs;
using Mediator;

namespace CmsApi.Server.Application.Features.Apps.Queries.GetAppById;

public sealed record GetAppByIdQuery(int AppId) : IQuery<Result<AppDto>>;
