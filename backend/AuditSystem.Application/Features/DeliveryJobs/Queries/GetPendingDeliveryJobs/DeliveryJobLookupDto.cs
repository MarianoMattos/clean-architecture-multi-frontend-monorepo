using System;

namespace AuditSystem.Application.Features.DeliveryJobs.Queries.GetPendingDeliveryJobs;

public record DeliveryJobLookupDto(
    Guid Id,
    string JobType,
    string Status,
    int RetryCount,
    DateTime CreatedAt
);