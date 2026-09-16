using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects;

public class ProjectName : ValueObject<string>
{
    public const int MAX_LENGTH = 200;
    public const int MIN_LENGTH = 3;

    private ProjectName(string value) : base(value) { }

    public static ProjectName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(ProjectErrors.Name.Empty);

        value = value.Trim();

        if (value.Length < MIN_LENGTH)
            throw new DomainException(
                string.Format(ProjectErrors.Name.TooShort, MIN_LENGTH));

        if (value.Length > MAX_LENGTH)
            throw new DomainException(
                string.Format(ProjectErrors.Name.TooLong, MAX_LENGTH));

        return new ProjectName(value);
    }
}
