using CmsApi.Application.Features.Auth.Commands.Register;
using CmsApi.Server.Application.Features.Auth.Commands.Login;
using Mediator;

namespace CmsApi.Server.Presentation.Endpoints.Auth;

public class AuthEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth")
            .WithTags("Auth");

        group.MapPost("register", async (
            RegisterCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Created(string.Empty, result.Value)
                : Results.BadRequest(new { error = result.Error });
        })
        .WithName("Register")
        .WithSummary("Register a new user")
        .AllowAnonymous();

        group.MapPost("login", async (
            LoginCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.Unauthorized();
        })
        .WithName("Login")
        .WithSummary("Authenticate and retrieve a JWT token")
        .AllowAnonymous();
    }
}
