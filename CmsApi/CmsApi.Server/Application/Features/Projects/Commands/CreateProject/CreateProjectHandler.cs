using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Projects.Commands.CreateProject;

public sealed class CreateProjectHandler(
    IUnitOfWork unitOfWork,
    IProjectRepository repository) : ICommandHandler<CreateProjectCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var newProject = Project.Create(command.Name, command.Description);

        if (await repository.ExistsByNameAsync(newProject.Name, cancellationToken))
            return Result<Unit>.Failure(string.Format(ProjectErrors.General.AlreadyExists, newProject.Name));

        await repository.AddAsync(newProject);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
