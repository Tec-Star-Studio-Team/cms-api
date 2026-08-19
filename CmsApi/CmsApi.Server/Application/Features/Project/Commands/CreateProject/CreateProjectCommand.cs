using Mediator;

namespace CmsApi.Server.Application.Features.Project.Commands.CreateProject;

public sealed record CreateProjectCommand(string Name, string Description) : ICommand;
