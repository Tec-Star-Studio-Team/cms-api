namespace CmsApi.Server.Application.Features.Apps.DTOs;

public sealed record AppDto(
    int Id,
    string Name,
    int ProjectId,
    int LanguageId,
    int TemplateId);
