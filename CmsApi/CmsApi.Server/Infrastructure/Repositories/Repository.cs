using CmsApi.Server.Domain.Common;
using CmsApi.Server.Domain.Interfaces.Repositories;
using CmsApi.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Server.Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(AppDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
        => await DbSet.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

    public async Task<TEntity> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(TEntity entity) => DbSet.Update(entity);

    public void Delete(TEntity entity) => DbSet.Remove(entity);
}
