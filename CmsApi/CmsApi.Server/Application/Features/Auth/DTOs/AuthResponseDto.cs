namespace CmsApi.Server.Application.Features.Auth.DTOs;

public sealed record AuthResponseDto(
    string Token,
    string Email,
    string FirstName,
    string LastName,
    DateTime ExpiresAt
);
