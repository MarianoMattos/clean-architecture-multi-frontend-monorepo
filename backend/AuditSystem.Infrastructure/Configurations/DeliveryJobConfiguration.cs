using AuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuditSystem.Infrastructure.Configurations;

public class DeliveryJobConfiguration : IEntityTypeConfiguration<DeliveryJob>
{
    public void Configure(EntityTypeBuilder<DeliveryJob> builder)
    {
        builder.ToTable("DeliveryJobs");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.JobCode)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.Property(d => d.ClientName)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(d => d.Status)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

        builder.HasIndex(d => d.JobCode)
            .IsUnique()
            .HasDatabaseName("UX_DeliveryJobs_JobCode");
    }
}