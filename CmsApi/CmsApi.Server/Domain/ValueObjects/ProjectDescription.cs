using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects;

public class ProjectDescription : ValueObject<string>
{
    public const int MaxLength = 2000;

    private ProjectDescription(string value) : base(value) { }

    public static ProjectDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new ProjectDescription(string.Empty);

        value = value.Trim();

        if (value.Length > MaxLength)
            throw new DomainException(
                string.Format(ProjectErrors.Description.TooLong, MaxLength));

        return new ProjectDescription(value);
    }
}
