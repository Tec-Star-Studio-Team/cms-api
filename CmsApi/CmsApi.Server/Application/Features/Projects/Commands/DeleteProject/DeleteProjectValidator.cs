using FluentValidation;

namespace CmsApi.Server.Application.Features.Projects.Commands.DeleteProject;

public sealed class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectValidator()
    {
        RuleFor(c => c.ProjectId)
            .GreaterThan(0)
            .WithMessage("Project ID is mandatory.");
    }
}
