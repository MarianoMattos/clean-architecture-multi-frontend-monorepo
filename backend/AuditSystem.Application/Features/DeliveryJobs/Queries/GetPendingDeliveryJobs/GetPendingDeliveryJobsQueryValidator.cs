using FluentValidation;

namespace AuditSystem.Application.Features.DeliveryJobs.Queries.GetPendingDeliveryJobs;

public class GetPendingDeliveryJobsQueryValidator : AbstractValidator<GetPendingDeliveryJobsQuery>
{
    public GetPendingDeliveryJobsQueryValidator()
    {
        RuleFor(x => x.MaxRetryCount)
            .GreaterThanOrEqualTo(0).WithMessage("Max retry count must be greater than or equal to 0.");
    }
}