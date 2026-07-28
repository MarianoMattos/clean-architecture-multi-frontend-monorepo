using FluentValidation;

namespace AuditSystem.Application.Common.Validation;

public static class CustomValidationRules
{
    public static IRuleBuilderOptions<T, string> ValidCodeFormat<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^[a-zA-Z0-9_-]+$")
            .WithMessage("'{PropertyName}' contains invalid characters. Only alphanumeric characters, hyphens, and underscores are allowed.");
    }
}