#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AuditSystem.Domain.Entities;

namespace AuditSystem.Domain.Contracts;

/// <summary>
/// Defines the read and write persistence operations for AuditLog entities.
/// </summary>
public interface IAuditLogRepository
{
    /// <summary>
    /// Persists a new audit log entry into the database.
    /// </summary>
    Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an audit log by its unique tracking identifier.
    /// </summary>
    Task<AuditLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of audit logs filtered by system source.
    /// </summary>
    Task<IReadOnlyList<AuditLog>> GetBySourceAsync(string systemSource, int page, int pageSize, CancellationToken cancellationToken = default);
}