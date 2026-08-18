using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.DTOs;
using Mediator;

namespace CmsApi.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<Result<AuthResponseDto>>;