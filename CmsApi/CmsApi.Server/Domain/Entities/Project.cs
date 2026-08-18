using CmsApi.Server.Domain.Common;

namespace CmsApi.Server.Domain.Entities;

public sealed class Project : BaseEntity<int>
{
    private Project() { }

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public static Project Create(string name, string description)
    {
        return new Project()
        {
            Name = name,
            Description = description
        };
    }
}
