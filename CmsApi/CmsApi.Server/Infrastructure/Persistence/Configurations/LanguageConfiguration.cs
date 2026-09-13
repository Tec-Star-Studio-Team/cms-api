using CmsApi.Server.Domain.Entities;
using CmsApi.Server.Domain.ValueObjects.Language;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CmsApi.Server.Infrastructure.Persistence.Configurations;

public sealed class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        // Table and schema
        builder.ToTable("Languages", "cms");

        // Primary key
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // Properties

        // OwnsOne — EF Core understands the Value Object structure
        // and can translate p.Name.Value in LINQ queries
        builder.OwnsOne(p => p.Code, code =>
        {
            code.Property(n => n.Value)
                .HasColumnName("Code") // ← stored as single column
                .IsRequired()
                .HasMaxLength(LanguageCode.MAX_LENGTH);
        });

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name") // ← stored as single column
                .IsRequired()
                .HasMaxLength(LanguageName.MAX_LENGTH);
        });

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt);
    }
}
