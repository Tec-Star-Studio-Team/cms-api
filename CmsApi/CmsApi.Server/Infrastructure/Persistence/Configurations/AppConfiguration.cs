using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmsApi.Server.Infrastructure.Persistence.Configurations;

public sealed class AppConfiguration : IEntityTypeConfiguration<App>
{
    public void Configure(EntityTypeBuilder<App> builder)
    {
        // Table and schema
        builder.ToTable("Apps", "cms");

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
                .HasMaxLength(AppName.MAX_LENGTH);
        });

        // Relationship with Project
        builder.HasOne<Project>()
            .WithMany()
            .HasForeignKey(a => a.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Language>()
            .WithMany()
            .HasForeignKey(a => a.LanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Template>()
            .WithMany()
            .HasForeignKey(a => a.TemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);
    }
}
