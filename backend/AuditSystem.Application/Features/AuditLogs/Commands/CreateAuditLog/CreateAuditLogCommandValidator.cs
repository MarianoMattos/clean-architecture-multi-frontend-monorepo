using AuditSystem.Application.Common.Validation;
using FluentValidation;

namespace AuditSystem.Application.Features.AuditLogs.Commands.CreateAuditLog;

public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
{
    public CreateAuditLogCommandValidator()
    {
        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .MaximumLength(100).WithMessage("Action must not exceed 100 characters.")
            .ValidCodeFormat();
    }
}