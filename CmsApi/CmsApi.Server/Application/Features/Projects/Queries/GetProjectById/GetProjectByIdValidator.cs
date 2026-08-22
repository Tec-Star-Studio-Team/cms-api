using FluentValidation;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetProjectById;

public sealed class GetProjectByIdValidator : AbstractValidator<GetProjectByIdQuery>
{
    public GetProjectByIdValidator()
    {
        RuleFor(prop => prop.ProjectId)
            .GreaterThan(0)
            .WithMessage("Please provide a valid project ID.");
    }
}
