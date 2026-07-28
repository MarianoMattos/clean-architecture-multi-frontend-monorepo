using FluentValidation;

namespace AuditSystem.Application.Features.AuditLogs.Queries.GetAuditLogsList;

public class GetAuditLogsListQueryValidator : AbstractValidator<GetAuditLogsListQuery>
{
    public GetAuditLogsListQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
    }
}