using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CmsApi.Server.Domain.ValueObjects.Language;

public sealed class LanguageCode : ValueObject<string>
{
    public const int MAX_LENGTH = 5;
    public const int MIN_LENGTH = 2;

    private LanguageCode(string value) : base(value) { }

    public static LanguageCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(LanguageErrors.Code.Empty);

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new DomainException(string.Format(LanguageErrors.Code.InvalidLength, MIN_LENGTH, MAX_LENGTH));

        if (!LanguageHelper.Exists(value))
            throw new DomainException(string.Format(LanguageErrors.Code.IsNotValid, value));

        value = value.Trim();
        value = Regex.Replace(value, @"\s+", "");

        return new LanguageCode(value);
    }
}
