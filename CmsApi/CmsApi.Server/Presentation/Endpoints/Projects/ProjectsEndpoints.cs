using CmsApi.Server.Application.Features.Project.Commands.CreateProject;
using Mediator;

namespace CmsApi.Server.Presentation.Endpoints.Projects;

public class ProjectsEndpoints : IEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/projects").WithTags("Project");

        group.MapPost("/", async (
            CreateProjectCommand command,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var result = await mediator.Send(command, cancellationToken);

            return Results.Created();
        })
        .WithName("Create")
        .WithSummary("Create a new project")
        .AllowAnonymous();
    }
}
