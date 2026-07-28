#nullable enable
using System;

namespace AuditSystem.Domain.Entities;

public class DeliveryJob : Entity
{
    public string JobCode { get; init; } = string.Empty;
    public string ClientName { get; init; } = string.Empty;
    public string Status { get; private set; } = "Pending";
    public int RetryCount { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private DeliveryJob() { }

    public static DeliveryJob Initialize(string jobCode, string clientName)
    {
        if (string.IsNullOrWhiteSpace(jobCode))
            throw new ArgumentException("El código de lote de entrega es obligatorio.", nameof(jobCode));

        return new DeliveryJob
         {
            JobCode = jobCode.ToUpperInvariant(),
            ClientName = clientName ?? throw new ArgumentNullException(nameof(clientName)),
            Status = "Pending",
            RetryCount = 0
         };
    }

    public void FailJob()
    {
        if (Status == "Completed")
            throw new InvalidOperationException("No se puede fallar un lote que ya ha sido completado con éxito.");

        RetryCount++;
        Status = "Failed";
    }

    public void CompleteJob()
    {
        if (Status == "Completed") return;

        Status = "Completed";
        CompletedAt = DateTime.UtcNow;
    }
}