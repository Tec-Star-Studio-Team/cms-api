using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Errors;
using CmsApi.Server.Domain.Exceptions;
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
        Validate(projectId, languageId, templateId);

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
        Validate(projectId, languageId, templateId);

        LanguageId = languageId;
        ProjectId = projectId;
        TemplateId = templateId;
        Name = AppName.Create(name);

        SetUpdatedAt();
    }

    private static void Validate(int projectId, int languageId, int templateId)
    {
        if (projectId < 0) throw new DomainException(ProjectErrors.General.InvalidId);

        if (languageId < 0) throw new DomainException(LanguageErrors.General.InvalidId);

        if (templateId < 0) throw new DomainException(TemplateErrors.General.InvalidId);
    }
}
