using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CmsApi.Server.Domain.ValueObjects.Language;

public sealed class LanguageName : ValueObject<string>
{
    public const int MAX_LENGTH = 25;
    public const int MIN_LENGTH = 3;

    private LanguageName(string value) : base(value) { }

    public static LanguageName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(LanguageErrors.Code.Empty);

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new DomainException(string.Format(LanguageErrors.Name.InvalidLength, MIN_LENGTH, MAX_LENGTH));

        value = Regex.Replace(value.Trim(), @"\s+", " ");

        return new LanguageName(value);
    }
}
