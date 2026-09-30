using CmsApi.Server.Application.Features.Templates.Commands;
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
    }
}
