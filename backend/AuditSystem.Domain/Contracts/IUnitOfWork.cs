#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuditSystem.Domain.Contracts;

/// <summary>
/// Contract for the Unit of Work pattern to manage transactional boundaries across repositories.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Commits all pending changes made in this unit of work to the database transactionally.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}