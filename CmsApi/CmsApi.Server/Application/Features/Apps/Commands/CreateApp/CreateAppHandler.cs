
using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Apps.Commands.CreateApp;

public sealed class CreateAppHandler(
    IUnitOfWork unitOfWork,
    IRepository<App, int> repository) : ICommandHandler<CreateAppCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(CreateAppCommand command, CancellationToken cancellationToken)
    {
        var newApp = App.Create(command.Name, command.ProjectId, command.LanguageId, command.TemplateId);

        await repository.AddAsync(newApp);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
