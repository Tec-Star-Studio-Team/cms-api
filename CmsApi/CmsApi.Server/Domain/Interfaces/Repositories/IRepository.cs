using CmsApi.Server.Domain.Common;

namespace CmsApi.Server.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
