using CmsApi.Domain.Interfaces;
using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Application.Features.Auth.DTOs;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Infrastructure.Settings;
using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CmsApi.Application.Features.Auth.Commands.Register;

public sealed class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthResponseDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public RegisterHandler(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async ValueTask<Result<AuthResponseDto>> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
            return Result<AuthResponseDto>.Failure("Email is already in use.");

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
            return Result<AuthResponseDto>.Failure(errors);
        }

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