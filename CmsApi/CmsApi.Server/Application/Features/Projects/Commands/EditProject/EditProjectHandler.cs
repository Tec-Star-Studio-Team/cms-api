using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.EditProject;

public sealed class EditProjectHandler(IUnitOfWork unitOfWork, IRepository<Project, int> repository) : ICommandHandler<EditProjectCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(EditProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (project is null)
            return Result<Unit>.NotFound($"Project with ID {command.Id} not found.");

        project.Update(command.Name, command.Description);

        repository.Update(project);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
