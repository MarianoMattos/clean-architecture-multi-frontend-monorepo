namespace AuditSystem.Domain.Entities;

public class DeliveryJob : Entity
{
    public string JobCode { get; private set; } = null!;
public string ClientName { get; private set; } = null!;
public string Status { get; private set; } = null!;
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
            ClientName = clientName,
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