using FluentValidation;

namespace CmsApi.Server.Application.Features.Apps.Queries.GetAppById;

public class GetAppByIdValidator : AbstractValidator<GetAppByIdQuery>
{
    public GetAppByIdValidator()
    {
        RuleFor(p => p.AppId)
            .GreaterThan(0)
            .WithMessage("Please provide a valid ID for the app.");
    }
}
