using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Templates.Commands.CreateTemplate;

public sealed class CreateTemplateHandler(IUnitOfWork unitOfWork, IRepository<Template, int> repository) : ICommandHandler<CreateTemplateCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(CreateTemplateCommand command, CancellationToken cancellationToken)
    {
        await repository.AddAsync(Template.Create(command.Name, command.Description), cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
