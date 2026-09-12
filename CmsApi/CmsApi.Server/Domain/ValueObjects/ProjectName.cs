using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects;

public class ProjectName : ValueObject<string>
{
    public const int MaxLength = 200;
    public const int MinLength = 3;

    private ProjectName(string value) : base(value) { }

    public static ProjectName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(ProjectErrors.Name.Empty);

        value = value.Trim();

        if (value.Trim().Length < MinLength)
            throw new DomainException(
                string.Format(ProjectErrors.Name.TooShort, MinLength));

        if (value.Trim().Length > MaxLength)
            throw new DomainException(
                string.Format(ProjectErrors.Name.TooLong, MaxLength));

        return new ProjectName(value);
    }
}
