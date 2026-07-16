using AuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditSystem.Infrastructure.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.SystemSource)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Action)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("varchar(150)");

        builder.Property(a => a.Payload)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(a => a.PerformedBy)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("varchar(100)");

        builder.Property(a => a.Severity)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(a => new { a.SystemSource, a.Severity })
            .HasDatabaseName("IX_AuditLogs_Source_Severity");
    }
}