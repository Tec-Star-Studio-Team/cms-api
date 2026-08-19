namespace CmsApi.Server.Domain.Common;

public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; protected set; } = default!;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }

    protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
}
