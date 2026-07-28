#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuditSystem.Domain.Contracts;
using AuditSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditSystem.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of the IDeliveryJobRepository contract.
/// </summary>
public class DeliveryJobRepository(AuditDbContext context) : IDeliveryJobRepository
{
    private readonly AuditDbContext _context = context;

    public async Task AddAsync(DeliveryJob deliveryJob, CancellationToken cancellationToken = default)
    {
        await _context.Set<DeliveryJob>().AddAsync(deliveryJob, cancellationToken);
    }

    public async Task<DeliveryJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<DeliveryJob>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(DeliveryJob deliveryJob, CancellationToken cancellationToken = default)
    {
        // EF Core tracks changes automatically if the entity was loaded in the same context instance.
        // We set the state to Modified explicitly to ensure compliance across disconnected scenarios.
        _context.Entry(deliveryJob).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<DeliveryJob>> GetPendingJobsAsync(int maxRetryCount, CancellationToken cancellationToken = default)
    {
        // Evaluates status using the string values defined in the Domain Entity
        return await _context.Set<DeliveryJob>()
            .Where(x => (x.Status == "Pending" || x.Status == "Failed") 
                        && x.RetryCount < maxRetryCount)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}