using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AuditSystem.Domain.Contracts;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CompleteDeliveryJob;

public class CompleteDeliveryJobCommandHandler(
    IDeliveryJobRepository repository, 
    IUnitOfWork unitOfWork) : IRequestHandler<CompleteDeliveryJobCommand, bool>
{
    private readonly IDeliveryJobRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<bool> Handle(CompleteDeliveryJobCommand request, CancellationToken cancellationToken)
    {
        var job = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (job == null) return false;

        job.CompleteJob(); 

        await _repository.UpdateAsync(job, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}