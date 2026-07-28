using AuditSystem.Application.Common.Validation;
using FluentValidation;

namespace AuditSystem.Application.Features.DeliveryJobs.Commands.CreateDeliveryJob;

public class CreateDeliveryJobCommandValidator : AbstractValidator<CreateDeliveryJobCommand>
{
    public CreateDeliveryJobCommandValidator()
    {
        RuleFor(x => x.JobCode)
            .NotEmpty().WithMessage("Job code is required.")
            .MaximumLength(50).WithMessage("Job code must not exceed 50 characters.")
            .ValidCodeFormat();;

        RuleFor(x => x.ClientName)
            .NotEmpty().WithMessage("Client name is required.")
            .MaximumLength(150).WithMessage("Client name must not exceed 150 characters.")
            .ValidCodeFormat();;
    }
}