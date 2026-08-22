using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.DeleteProject;

public sealed class DeleteProjectHandler(IUnitOfWork unitOfWork, IRepository<Project, int> repository) : ICommandHandler<DeleteProjectCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(command.ProjectId, cancellationToken);
        if (project is null)
            return Result<Unit>.NotFound($"Project with ID {command.ProjectId} not found.");

        repository.Delete(project);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
