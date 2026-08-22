using CmsApi.Server.Application.Features.Projects.Commands.CreateProject;
using CmsApi.Server.Application.Features.Projects.Commands.DeleteProject;
using CmsApi.Server.Application.Features.Projects.Commands.EditProject;
using CmsApi.Server.Application.Features.Projects.Queries.GetProjectById;
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
        .RequireAuthorization();

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
        .RequireAuthorization();

        group.MapDelete("/{id}", async (int id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            var command = new DeleteProjectCommand(id);
            var validator = new DeleteProjectValidator();
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        })
        .WithName("Delete")
        .WithSummary("Delete by ID")
        .RequireAuthorization();

        group.MapPut("/{id}", async (int id, EditProjectCommand command, IMediator mediator, CancellationToken cancellationToken) =>
        {
            command.Id = id;
            var validator = new EditProjectValidator();
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var result = await mediator.Send(command);
            return result.ToHttpResult();
        })
        .WithName("Edit")
        .WithSummary("Edit an existing project")
        .RequireAuthorization();
    }
}
