using FluentValidation;

namespace CmsApi.Server.Application.Features.Projects.Queries.GetPaginatedProjects;

public sealed class GetPaginatedProjectsValidator : AbstractValidator<GetPaginatedProjectsQuery>
{
    public GetPaginatedProjectsValidator()
    {
        RuleFor(p => p.PageSize)
            .GreaterThan(0)
            .WithMessage("The page size must be greater than zero.");
    }
}
