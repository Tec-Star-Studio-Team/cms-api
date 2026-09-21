using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;

namespace CmsApi.Server.Domain.ValueObjects.Template;

public class TemplateDescription : ValueObject<string>
{
    public const int MAX_LENGTH = 2000;
    public const int MIN_LENGTH = 30;

    private TemplateDescription(string value) : base(value) { }

    public static TemplateDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(TemplateErrors.Description.Empty);

        value = value.Trim();

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new DomainException(string.Format(TemplateErrors.Description.InvalidLength, MIN_LENGTH, MAX_LENGTH));

        return new TemplateDescription(value);
    }
}
