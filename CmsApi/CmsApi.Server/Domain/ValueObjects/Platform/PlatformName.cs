using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects.Platform;

public class PlatformName : ValueObject<string>
{
    public const int MIN_LENGTH = 3;
    public const int MAX_LENGTH = 100;

    private PlatformName(string value) : base(value) { }

    public static PlatformName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(PlatformErrors.Name.Empty);

        value = value.Trim();

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new DomainException(string.Format(PlatformErrors.Name.InvalidLength, MIN_LENGTH, MAX_LENGTH));

        return new PlatformName(value);
    }
}
