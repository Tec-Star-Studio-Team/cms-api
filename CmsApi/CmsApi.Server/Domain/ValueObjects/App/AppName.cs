using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects.App;

public class AppName : ValueObject<string>
{
    public const int MIN_LENGTH = 3;
    public const int MAX_LENGTH = 100;

    private AppName(string value) : base(value) { }

    public static AppName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(AppErrors.Name.Empty);

        value = value.Trim();

        if (value.Length < MIN_LENGTH)
            throw new DomainException(string.Format(AppErrors.Name.TooShort, MIN_LENGTH));

        if (value.Length > MAX_LENGTH)
            throw new DomainException(string.Format(AppErrors.Name.TooLong, MAX_LENGTH));

        return new AppName(value);
    }
}
