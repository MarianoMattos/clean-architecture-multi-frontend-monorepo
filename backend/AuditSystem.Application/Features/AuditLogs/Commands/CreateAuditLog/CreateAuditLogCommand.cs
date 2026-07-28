using System;
using MediatR;
using AuditSystem.Domain.Enums;

namespace AuditSystem.Application.Features.AuditLogs.Commands.CreateAuditLog;

public record CreateAuditLogCommand(
    string SystemSource,
    string Action,
    string Payload,
    AuditSeverity Severity,
    string? PerformedBy
) : IRequest<Guid>;