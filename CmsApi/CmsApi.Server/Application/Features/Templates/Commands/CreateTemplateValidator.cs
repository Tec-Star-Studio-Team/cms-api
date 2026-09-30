using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.ValueObjects.Template;
using FluentValidation;

namespace CmsApi.Server.Application.Features.Templates.Commands;

public sealed class CreateTemplateValidator : AbstractValidator<CreateTemplateCommand>
{
    public CreateTemplateValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(TemplateErrors.Name.Empty)
            .Length(TemplateName.MIN_LENGTH, TemplateName.MAX_LENGTH)
            .WithMessage(string.Format(TemplateErrors.Name.InvalidLength, TemplateName.MIN_LENGTH, TemplateName.MAX_LENGTH));

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage(TemplateErrors.Description.Empty)
            .Length(TemplateDescription.MIN_LENGTH, TemplateDescription.MAX_LENGTH)
            .WithMessage(string.Format(TemplateErrors.Description.InvalidLength, TemplateDescription.MIN_LENGTH, TemplateDescription.MAX_LENGTH));
    }
}
