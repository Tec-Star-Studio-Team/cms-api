using CmsApi.Domain.Interfaces;
using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.Commands.Login;
using CmsApi.Server.Application.Features.Auth.DTOs;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Infrastructure.Settings;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CmsApi.Application.Features.Auth.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResponseDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async ValueTask<Result<AuthResponseDto>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Result<AuthResponseDto>.Failure("Invalid email or password.");

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

        return Result<AuthResponseDto>.Success(new AuthResponseDto(
            Token: token,
            Email: user.Email!,
            FirstName: user.FirstName,
            LastName: user.LastName,
            ExpiresAt: expiresAt
        ));
    }
}