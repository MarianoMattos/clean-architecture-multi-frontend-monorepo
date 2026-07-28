using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AuditSystem.Domain.Contracts;

namespace AuditSystem.Application.Features.DeliveryJobs.Queries.GetPendingDeliveryJobs;

public class GetPendingDeliveryJobsQueryHandler(IDeliveryJobRepository repository) 
    : IRequestHandler<GetPendingDeliveryJobsQuery, IEnumerable<DeliveryJobLookupDto>>
{
    private readonly IDeliveryJobRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private const int MaxRetryCountThreshold = 5;

    public async Task<IEnumerable<DeliveryJobLookupDto>> Handle(GetPendingDeliveryJobsQuery request, CancellationToken cancellationToken)
    {
        var pendingJobs = await _repository.GetPendingJobsAsync(MaxRetryCountThreshold, cancellationToken);

        return pendingJobs.Select(job => new DeliveryJobLookupDto(
            job.Id,
            job.ClientName,
            job.Status,
            job.RetryCount,
            job.CreatedAt
        ));
    }
}