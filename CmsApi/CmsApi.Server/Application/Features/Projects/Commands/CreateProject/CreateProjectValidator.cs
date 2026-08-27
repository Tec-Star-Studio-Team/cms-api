using FluentValidation;

namespace CmsApi.Server.Application.Features.Projects.Commands.CreateProject;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        var nameMaxLength = 200;
        var descriptionMaxLength = 2000;

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Project name is mandatory.")
            .MaximumLength(nameMaxLength)
            .WithMessage($"The maximum length is {nameMaxLength}");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Project description is mandatory.")
            .MaximumLength(descriptionMaxLength)
            .WithMessage($"The maximum length is {descriptionMaxLength}");
    }
}
