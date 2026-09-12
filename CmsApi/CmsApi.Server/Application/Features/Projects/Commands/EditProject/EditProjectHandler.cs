using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.EditProject;

public sealed class EditProjectHandler(IUnitOfWork unitOfWork, IProjectRepository repository) : ICommandHandler<EditProjectCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(EditProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (project is null)
            return Result<Unit>.NotFound(string.Format(ProjectErrors.General.NotFound, command.Id));

        if (await repository.ExistsByNameAsync(project.Name, cancellationToken))
            Result<Unit>.Failure(string.Format(ProjectErrors.General.AlreadyExists, project.Name));

        project.Update(command.Name, command.Description);

        repository.Update(project);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
