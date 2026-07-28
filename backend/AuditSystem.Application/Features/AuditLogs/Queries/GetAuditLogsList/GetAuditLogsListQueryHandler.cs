using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AuditSystem.Domain.Contracts;

namespace AuditSystem.Application.Features.AuditLogs.Queries.GetAuditLogsList;

public class GetAuditLogsListQueryHandler(IAuditLogRepository repository) 
    : IRequestHandler<GetAuditLogsListQuery, IEnumerable<AuditLogLookupDto>>
{
    private readonly IAuditLogRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<AuditLogLookupDto>> Handle(GetAuditLogsListQuery request, CancellationToken cancellationToken)
    {
        var logs = await _repository.GetBySourceAsync(request.SystemSource, request.Page, request.PageSize, cancellationToken);

        return logs.Select(log => new AuditLogLookupDto(
            log.Id,
            log.SystemSource,
            log.Action,
            log.Payload,
            log.Severity,
            log.PerformedBy,
            log.CreatedAt
        ));
    }
}