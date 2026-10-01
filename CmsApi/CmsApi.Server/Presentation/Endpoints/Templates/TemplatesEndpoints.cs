using CmsApi.Server.Application.Features.Templates.Commands.CreateTemplate;
using CmsApi.Server.Application.Features.Templates.Commands.DeleteTemplate;
using CmsApi.Server.Presentation.Extensions;
using FluentValidation;
using Mediator;

namespace CmsApi.Server.Presentation.Endpoints.Projects;

public class TemplatesEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/templates").WithTags("Template");

        group.MapPost("/", async (
            CreateTemplateCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Results.Created() : result.ToHttpResult();
        })
        .WithName("Create Template")
        .WithSummary("Create a new template")
        .RequireAuthorization();

        group.MapDelete("/{id}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new DeleteTemplateCommand(id);
            var validator = new DeleteTemplateValidator();
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        })
        .WithName("Delete template")
        .WithSummary("Delete a template by ID");
    }
}
