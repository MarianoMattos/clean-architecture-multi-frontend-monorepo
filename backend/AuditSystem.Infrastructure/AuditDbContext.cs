using AuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuditSystem.Infrastructure;

public class AuditDbContext : DbContext
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
    {
    }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<DeliveryJob> DeliveryJobs => Set<DeliveryJob>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}