using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.DTOs;
using Mediator;

namespace CmsApi.Server.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password
) : IRequest<Result<AuthResponseDto>>;
