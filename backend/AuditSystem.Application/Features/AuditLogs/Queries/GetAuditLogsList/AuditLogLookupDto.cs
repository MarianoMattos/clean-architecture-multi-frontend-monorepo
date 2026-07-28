using System;
using AuditSystem.Domain.Enums;

namespace AuditSystem.Application.Features.AuditLogs.Queries.GetAuditLogsList;

public record AuditLogLookupDto(
    Guid Id,
    string SystemSource,
    string Action,
    string Payload,
    AuditSeverity Severity,
    string PerformedBy,
    DateTime CreatedAt
);