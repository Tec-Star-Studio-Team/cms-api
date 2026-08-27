using CmsApi.Server.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CmsApi.Tests.Common;

public sealed class CmsApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public CmsApiFactory(string connectionString)
        => _connectionString = connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the real DbContext registration
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.RemoveAll<AppDbContext>();

            // Replace with the TestContainers connection string
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_connectionString));
        });

        builder.UseEnvironment("Testing");
    }

    public async Task ResetDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Projects.ExecuteDeleteAsync();
        await db.Users.ExecuteDeleteAsync();
    }
}
