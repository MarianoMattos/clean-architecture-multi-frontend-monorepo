using System.Collections.Generic;
using MediatR;

namespace AuditSystem.Application.Features.DeliveryJobs.Queries.GetPendingDeliveryJobs;

public record GetPendingDeliveryJobsQuery(int MaxRetryCount) : IRequest<IEnumerable<DeliveryJobLookupDto>>;