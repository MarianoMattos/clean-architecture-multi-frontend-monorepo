using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AuditSystem.Domain.Contracts;
using AuditSystem.Domain.Entities;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CreateDeliveryJob;

public class CreateDeliveryJobCommandHandler(
    IDeliveryJobRepository repository, 
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDeliveryJobCommand, Guid>
{
    private readonly IDeliveryJobRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> Handle(CreateDeliveryJobCommand request, CancellationToken cancellationToken)
    {
        var job = DeliveryJob.Initialize(request.JobCode, request.ClientName);

        await _repository.AddAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}