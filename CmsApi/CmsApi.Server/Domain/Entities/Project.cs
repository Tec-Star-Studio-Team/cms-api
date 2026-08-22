using CmsApi.Server.Domain.Common;

namespace CmsApi.Server.Domain.Entities;

public sealed class Project : BaseEntity<int>
{
    public Project() { }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public static Project Create(string name, string description)
    {
        return new Project()
        {
            Name = name,
            Description = description
        };
    }

    public void Update(string name, string description)
    {
        this.Name = name;
        this.Description = description;
        this.SetUpdatedAt();
    }
}
