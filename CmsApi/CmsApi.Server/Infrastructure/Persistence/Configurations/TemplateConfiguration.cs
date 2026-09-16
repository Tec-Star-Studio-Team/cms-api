using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmsApi.Server.Infrastructure.Persistence.Configurations;

public sealed class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        // Table and schema
        builder.ToTable("Templates", "cms");

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
                .HasMaxLength(LanguageCode.MAX_LENGTH);
        });

        builder.OwnsOne(p => p.Description, description =>
        {
            description.Property(n => n.Value)
                .HasColumnName("Description") // ← stored as single column
                .HasMaxLength(LanguageName.MAX_LENGTH);
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);
    }
}
