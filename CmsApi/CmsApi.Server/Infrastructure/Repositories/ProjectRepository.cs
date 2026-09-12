using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.Interfaces.Repositories;
using CmsApi.Server.Domain.ValueObjects;
using CmsApi.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CmsApi.Server.Infrastructure.Repositories;

public class ProjectRepository : Repository<Project, int>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByNameAsync(ProjectName name, CancellationToken cancellationToken)
    {
        return await Context.Projects
            .AsNoTracking()
            .AnyAsync(p => p.Name.Value.ToLower() == name.Value.ToLower(), cancellationToken);
    }
}
