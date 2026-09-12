using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmsApi.Server.Infrastructure.Persistence.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        // Table and schema
        builder.ToTable("Projects", "cms");

        // Primary key
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Properties

        // OwnsOne — EF Core understands the Value Object structure
        // and can translate p.Name.Value in LINQ queries
        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name") // ← stored as single column
                .IsRequired()
                .HasMaxLength(ProjectName.MaxLength);
        });

        builder.OwnsOne(p => p.Description, desc =>
        {
            desc.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(ProjectDescription.MaxLength);
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);

        // Indexes
        /*builder.HasIndex(p => p.OwnerId);
        builder.HasIndex(p => p.IsDeleted);
        builder.HasIndex(p => new { p.OwnerId, p.IsDeleted }); // composite — common query pattern*/

        // Relationships
        /*builder.HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete*/
    }
}
