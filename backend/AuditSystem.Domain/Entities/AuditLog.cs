using AuditSystem.Domain.Enums;

namespace AuditSystem.Domain.Entities;

public class AuditLog : Entity
{
    public string SystemSource { get; private set; } = null!;
public string Action { get; private set; } = null!;
public string Payload { get; private set; } = null!;
public AuditSeverity Severity { get; private set; }
public string PerformedBy { get; private set; } = null!;
    private AuditLog() { }

    public static AuditLog Create(string source, string action, string payload, AuditSeverity severity, string performedBy)
    {
        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("El origen del sistema es requerido para auditar.", nameof(source));

        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("La acción auditada no puede estar vacía.", nameof(action));

        return new AuditLog
        {
            SystemSource = source,
            Action = action,
            Payload = payload,
            Severity = severity,
            PerformedBy = performedBy ?? "System"
        };
    }
}