using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.ValueObjects.Template;

namespace CmsApi.Server.Domain.Entities;

public sealed class Template : BaseEntity<int>
{
    public TemplateName Name { get; private set; } = null!;
    public TemplateDescription Description { get; private set; } = null!;

    public static Template Create(string name, string description)
    {
        return new Template()
        {
            Name = TemplateName.Create(name),
            Description = TemplateDescription.Create(description)
        };
    }

    public void Update(string name, string description)
    {
        Name = TemplateName.Create(name);
        Description = TemplateDescription.Create(description);

        SetUpdatedAt();
    }
}
