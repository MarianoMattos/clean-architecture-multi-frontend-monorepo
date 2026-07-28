using AuditSystem.Domain.Contracts;
using AuditSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuditDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAuditLogRepository, AuditLogRepository>();
        services.AddScoped<IDeliveryJobRepository, DeliveryJobRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}