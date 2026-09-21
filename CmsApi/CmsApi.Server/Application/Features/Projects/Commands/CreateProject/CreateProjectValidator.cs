using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.ValueObjects;
using FluentValidation;

namespace CmsApi.Server.Application.Features.Projects.Commands.CreateProject;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ProjectErrors.Name.Empty)
            .Length(ProjectName.MIN_LENGTH, ProjectName.MAX_LENGTH)
            .WithMessage(string.Format(ProjectErrors.Name.InvalidLength, ProjectName.MIN_LENGTH, ProjectName.MAX_LENGTH));

        RuleFor(c => c.Description)
            .MaximumLength(ProjectDescription.MAX_LENGTH)
            .WithMessage(string.Format(ProjectErrors.Description.InvalidLength, ProjectDescription.MAX_LENGTH));
    }
}
