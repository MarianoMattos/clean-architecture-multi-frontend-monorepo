#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuditSystem.Domain.Entities;

namespace AuditSystem.Domain.Contracts;

/// <summary>
/// Defines the persistence operations for managing and tracking database and application delivery jobs.
/// </summary>
public interface IDeliveryJobRepository
{
    /// <summary>
    /// Persists a new delivery job entry.
    /// </summary>
    Task AddAsync(DeliveryJob deliveryJob, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific delivery job by its unique identifier.
    /// </summary>
    Task<DeliveryJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the state and telemetry details of an existing delivery job.
    /// </summary>
    Task UpdateAsync(DeliveryJob deliveryJob, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all delivery jobs that are currently pending execution or eligible for retry.
    /// </summary>
    Task<IReadOnlyList<DeliveryJob>> GetPendingJobsAsync(int maxRetryCount, CancellationToken cancellationToken = default);
}