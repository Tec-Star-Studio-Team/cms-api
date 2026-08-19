using CmsApi.Server.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CmsApi.Server.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Redirect all Identity tables to the 'access' schema
        builder.Entity<ApplicationUser>().ToTable("Users", "access");
        builder.Entity<IdentityRole<Guid>>().ToTable("Roles", "access");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "access");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "access");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "access");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "access");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "access");

        // CMS entities use the 'cms' schema (configured via IEntityTypeConfiguration<T>)
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
