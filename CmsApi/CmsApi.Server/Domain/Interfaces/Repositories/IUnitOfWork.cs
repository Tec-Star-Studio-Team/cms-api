namespace CmsApi.Server.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}