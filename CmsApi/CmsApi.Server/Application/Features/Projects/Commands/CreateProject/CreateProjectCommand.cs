using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.CreateProject;

public sealed record CreateProjectCommand(string Name, string Description) : ICommand;
