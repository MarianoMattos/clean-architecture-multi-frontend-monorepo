using System;
using MediatR;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CompleteDeliveryJob;

public record CompleteDeliveryJobCommand(Guid Id) : IRequest<bool>;