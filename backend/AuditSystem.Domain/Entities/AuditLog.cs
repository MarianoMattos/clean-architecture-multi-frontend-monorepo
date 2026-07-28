#nullable enable
using System;
using AuditSystem.Domain.Enums;

namespace AuditSystem.Domain.Entities;

public class AuditLog : Entity
{
    public string SystemSource { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty;
    public string Payload { get; init; } = string.Empty;
    public AuditSeverity Severity { get; init; }
    public string PerformedBy { get; init; } = string.Empty;

    private AuditLog() { }

    public static AuditLog Create(string source, string action, string payload, AuditSeverity severity, string? performedBy)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("El origen del sistema es requerido para auditar.", nameof(source));

        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("La acción auditada no puede estar vacía.", nameof(action));

        return new AuditLog
         {
            SystemSource = source,
            Action = action,
            Payload = payload ?? string.Empty,
            Severity = severity,
            PerformedBy = performedBy ?? "System"
         };
    }
}