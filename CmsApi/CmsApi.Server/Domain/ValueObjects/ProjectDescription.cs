using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects;

public class ProjectDescription : ValueObject<string>
{
    public const int MAX_LENGTH = 2000;

    private ProjectDescription(string value) : base(value) { }

    public static ProjectDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new ProjectDescription(string.Empty);

        value = value.Trim();

        if (value.Length > MAX_LENGTH)
            throw new DomainException(
                string.Format(ProjectErrors.Description.TooLong, MAX_LENGTH));

        return new ProjectDescription(value);
    }
}
