#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using AuditSystem.Domain.Contracts;

namespace AuditSystem.Infrastructure.Repositories;

/// <summary>
/// Entity Framework Core implementation of the IUnitOfWork contract.
/// </summary>
public class UnitOfWork(AuditDbContext context) : IUnitOfWork
{
    private readonly AuditDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private bool _disposed;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}