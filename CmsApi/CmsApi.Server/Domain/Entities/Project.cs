using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.ValueObjects;

namespace CmsApi.Server.Domain.Entities;

public sealed class Project : BaseEntity<int>
{
    public Project() { }

    public ProjectName Name { get; private set; } = null!;

    public ProjectDescription Description { get; private set; } = null!;

    public static Project Create(string name, string description)
    {
        return new Project()
        {
            Name = ProjectName.Create(name),
            Description = ProjectDescription.Create(description)
        };
    }

    public void Update(string name, string description)
    {
        Name = ProjectName.Create(name);
        Description = ProjectDescription.Create(description);

        SetUpdatedAt();
    }
}
