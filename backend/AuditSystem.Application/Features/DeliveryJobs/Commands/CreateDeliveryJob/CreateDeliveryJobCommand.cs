using System;
using MediatR;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CreateDeliveryJob;

public record CreateDeliveryJobCommand(
    string JobCode,
    string ClientName
) : IRequest<Guid>;