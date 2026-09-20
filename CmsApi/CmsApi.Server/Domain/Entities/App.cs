using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.ValueObjects.App;

namespace CmsApi.Server.Domain.Entities;

public class App : BaseEntity<int>
{
    public AppName Name { get; private set; } = null!;

    public int ProjectId { get; private set; }

    public int LanguageId { get; private set; }

    public int TemplateId { get; private set; }

    public static App Create(string name, int projectId, int languageId, int templateId)
    {
        return new App()
        {
            LanguageId = languageId,
            ProjectId = projectId,
            TemplateId = templateId,
            Name = AppName.Create(name),
        };
    }

    public void Update(string name, int projectId, int languageId, int templateId)
    {
        LanguageId = languageId;
        ProjectId = projectId;
        TemplateId = templateId;
        Name = AppName.Create(name);

        SetUpdatedAt();
    }
}
