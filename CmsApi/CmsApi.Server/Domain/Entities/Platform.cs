using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.ValueObjects.Platform;

namespace CmsApi.Server.Domain.Entities;

public class Platform : BaseEntity<int>
{
    public PlatformName Name { get; private set; } = null!;

    public static Platform Create(string name)
    {
        return new Platform()
        {
            Name = PlatformName.Create(name)
        };
    }

    public void Update(string name)
    {
        Name = PlatformName.Create(name);

        SetUpdatedAt();
    }
}
