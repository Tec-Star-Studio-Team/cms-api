
using CmsApi.Server.Application.Common.Models;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.DeleteProject;

public sealed record DeleteProjectCommand(int ProjectId) : ICommand<Result<Unit>>;
