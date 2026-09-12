using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects;

namespace CmsApi.Server.Domain.Interfaces.Repositories;

public interface IProjectRepository : IRepository<Project, int>
{
    Task<bool> ExistsByNameAsync(ProjectName name, CancellationToken cancellationToken);
}
