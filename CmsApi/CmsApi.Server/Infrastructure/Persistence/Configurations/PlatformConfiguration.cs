using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects.Platform;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmsApi.Server.Infrastructure.Persistence.Configurations;

public sealed class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        // Table and schema
        builder.ToTable("Platforms", "cms");

        // Primary key
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Properties

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name") // ← stored as single column
                .IsRequired()
                .HasMaxLength(PlatformName.MAX_LENGTH);
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);
    }
}
