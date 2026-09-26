using CmsApi.Server.Application.Features.Apps.Commands.CreateApp;
using CmsApi.Server.Application.Features.Apps.Queries.GetAppById;
using CmsApi.Server.Presentation.Extensions;
using FluentValidation;
using Mediator;

namespace CmsApi.Server.Presentation.Endpoints.Projects;

public class AppsEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/apps").WithTags("App");

        group.MapPost("/", async (
            CreateAppCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Results.Created() : result.ToHttpResult();
        })
        .WithName("Create App")
        .WithSummary("Create a new app")
        .RequireAuthorization();

        group.MapGet("/{id}", async (
            int id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAppByIdQuery(id);
            var validator = new GetAppByIdValidator();
            await validator.ValidateAndThrowAsync(query, cancellationToken);

            var result = await mediator.Send(query, cancellationToken);

            return result.ToHttpResult();
        })
        .WithName("Get App")
        .WithSummary("Get by ID")
        .RequireAuthorization();
    }
}
