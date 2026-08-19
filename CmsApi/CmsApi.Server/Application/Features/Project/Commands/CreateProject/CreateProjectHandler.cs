using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Project.Commands.CreateProject;

public sealed class CreateProjectHandler(
    IUnitOfWork unitOfWork,
    IRepository<Domain.Entities.Project, int> projectRepository) : ICommandHandler<CreateProjectCommand>
{
    public async ValueTask<Unit> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        await projectRepository.AddAsync(Domain.Entities.Project.Create(command.Name, command.Description));
        await unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}
