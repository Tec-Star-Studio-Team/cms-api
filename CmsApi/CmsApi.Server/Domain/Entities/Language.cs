using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.ValueObjects.Language;

namespace CmsApi.Server.Domain.Entities;

public sealed class Language : BaseEntity<int>
{
    public Language() { }

    public LanguageCode Code { get; private set; } = null!;

    public LanguageName Name { get; private set; } = null!;

    public static Language Create(string code, string name)
    {
        return new Language()
        {
            Code = LanguageCode.Create(code),
            Name = LanguageName.Create(name)
        };
    }

    public void Update(string code, string name)
    {
        Code = LanguageCode.Create(code);
        Name = LanguageName.Create(name);

        SetUpdatedAt();
    }
}
