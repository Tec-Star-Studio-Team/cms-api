using CmsApi.Server.Application.Common.Models;
using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Interfaces.Repositories;
using Mediator;

namespace CmsApi.Server.Application.Features.Templates.Commands.DeleteTemplate;

public sealed class DeleteTemplateHandler(IUnitOfWork unitOfWork, IRepository<Template, int> repository) : ICommandHandler<DeleteTemplateCommand, Result<Unit>>
{
    public async ValueTask<Result<Unit>> Handle(DeleteTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = await repository.GetByIdAsync(command.Id, cancellationToken);
        if (template is null)
            return Result<Unit>.NotFound(string.Format(TemplateErrors.General.NotFound, command.Id));

        repository.Delete(template);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result<Unit>.Success();
    }
}
