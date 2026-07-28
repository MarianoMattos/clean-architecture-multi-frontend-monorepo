using System.Collections.Generic;
using MediatR;

namespace AuditSystem.Application.Features.AuditLogs.Queries.GetAuditLogsList;

public record GetAuditLogsListQuery(
    string SystemSource, 
    int Page, 
    int PageSize
) : IRequest<IEnumerable<AuditLogLookupDto>>;