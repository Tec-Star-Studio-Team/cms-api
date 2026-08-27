using CmsApi.Server.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace CmsApi.Tests.Common;

public sealed class SqlServerContainerFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder(ContainerConfiguration.Image)
        .WithPassword(ContainerConfiguration.Password)
        .Build();

    public AppDbContext DbContext { get; private set; } = null!;
    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        DbContext = new AppDbContext(options);

        // Run all real migrations against the container
        await DbContext.Database.MigrateAsync();
    }

    public async Task ResetAsync()
    {
        await DbContext.Projects.ExecuteDeleteAsync();
        await DbContext.Users.ExecuteDeleteAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.DisposeAsync();
    }
}

// Collection definition — shares the fixture across all tests in the collection
[CollectionDefinition(nameof(SqlServerCollection))]
public sealed class SqlServerCollection : ICollectionFixture<SqlServerContainerFixture>
{ }
