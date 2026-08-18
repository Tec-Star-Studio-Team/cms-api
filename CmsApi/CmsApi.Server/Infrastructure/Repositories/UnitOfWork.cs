using CmsApi.Server.Domain.Interfaces.Repositories;
using CmsApi.Server.Infrastructure.Persistence;

namespace CmsApi.Server.Infrastructure.Repositories;

public sealed class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
