using CmsApi.Server.Application.Features.Project.Commands.CreateProject;
using CmsApi.Server.Application.Features.Project.Queries.GetProjectById;
using CmsApi.Server.Presentation.Extensions;
using FluentValidation;
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
        .AllowAnonymous(); // Remove after testing

        group.MapGet("/{id}", async (
            int id,
            IMediator mediator,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProjectByIdQuery(id);
            var validator = new GetProjectByIdValidator();
            await validator.ValidateAndThrowAsync(query, cancellationToken);

            var result = await mediator.Send(query, cancellationToken);

            return result.ToHttpResult();
        })
        .WithName("Get")
        .WithSummary("Get by ID")
        .AllowAnonymous();
        //.RequireAuthorization();
    }
}
