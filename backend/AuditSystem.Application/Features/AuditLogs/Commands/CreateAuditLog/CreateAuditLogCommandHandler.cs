using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AuditSystem.Domain.Contracts;
using AuditSystem.Domain.Entities;

namespace AuditSystem.Application.Features.AuditLogs.Commands.CreateAuditLog;

public class CreateAuditLogCommandHandler(
    IAuditLogRepository repository, 
    IUnitOfWork unitOfWork) : IRequestHandler<CreateAuditLogCommand, Guid>
    {
        private readonly IAuditLogRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<Guid> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var auditLog = AuditLog.Create(
                request.SystemSource,
                request.Action,
                request.Payload,
                request.Severity,
                request.PerformedBy
            );

            await _repository.AddAsync(auditLog, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return auditLog.Id;
        }
    }