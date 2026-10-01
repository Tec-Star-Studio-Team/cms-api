using CmsApi.Server.Domain.Errors;
using FluentValidation;

namespace CmsApi.Server.Application.Features.Templates.Commands.DeleteTemplate;

public sealed class DeleteTemplateValidator : AbstractValidator<DeleteTemplateCommand>
{
    public DeleteTemplateValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
            .WithMessage(TemplateErrors.General.InvalidId);
    }
}
