using System;
using FluentValidation;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CompleteDeliveryJob;

public class CompleteDeliveryJobCommandValidator : AbstractValidator<CompleteDeliveryJobCommand>
{
    public CompleteDeliveryJobCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Delivery job ID is required and must be a valid GUID.");
    }
}