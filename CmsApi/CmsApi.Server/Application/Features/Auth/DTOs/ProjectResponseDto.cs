namespace CmsApi.Server.Application.Features.Auth.DTOs;

public sealed record ProjectResponseDto(
    int Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
