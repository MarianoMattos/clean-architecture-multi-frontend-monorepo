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
/// Entity Framework Core implementation of the IAuditLogRepository contract.
/// </summary>
public class AuditLogRepository(AuditDbContext context) : IAuditLogRepository
{
    private readonly AuditDbContext _context = context;

    public async Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken = default)
    {
        await _context.Set<AuditLog>().AddAsync(auditLog, cancellationToken);
    }

    public async Task<AuditLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<AuditLog>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AuditLog>> GetBySourceAsync(
        string systemSource, 
        int page, 
        int pageSize, 
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<AuditLog>()
            .AsNoTracking()
            .Where(x => x.SystemSource == systemSource)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}