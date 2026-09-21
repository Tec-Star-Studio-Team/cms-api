using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.ValueObjects.App;
using FluentValidation;

namespace CmsApi.Server.Application.Features.Apps.Commands.CreateApp;

public sealed class CreateAppValidator : AbstractValidator<CreateAppCommand>
{
    public CreateAppValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(AppErrors.Name.Empty)
            .Length(AppName.MIN_LENGTH, AppName.MAX_LENGTH)
            .WithMessage(string.Format(AppErrors.Name.InvalidLength, AppName.MIN_LENGTH, AppName.MAX_LENGTH));

        RuleFor(c => c.ProjectId)
            .GreaterThan(0)
            .WithMessage(ProjectErrors.General.InvalidId);

        RuleFor(c => c.LanguageId)
            .GreaterThan(0)
            .WithMessage(LanguageErrors.General.InvalidId);

        RuleFor(c => c.TemplateId)
            .GreaterThan(0)
            .WithMessage(TemplateErrors.General.InvalidId);
    }
}
