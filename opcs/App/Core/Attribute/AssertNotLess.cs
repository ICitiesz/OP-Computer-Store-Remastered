using System.ComponentModel.DataAnnotations;
using opcs.Resources;

namespace opcs.App.Core.Attribute;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class AssertNotLess(string? errorMessage = null, bool errorSetDefault = false, int targetValue = 0) : ValidationAttribute
{
    private readonly string _errorMessage = errorMessage ?? CodeMessages.opcs_error_request_value_not_found;

    protected override ValidationResult? IsValid(object? currentFieldValue, ValidationContext validationContext)
    {
        if (currentFieldValue == null) return new ValidationResult(CodeMessages.opcs_error_request_empty_property);

        if (!((int) currentFieldValue < targetValue))
        {
            return ValidationResult.Success;
        }

        if (!errorSetDefault)
        {
            return new ValidationResult(_errorMessage);
        }

        validationContext.ObjectType.GetProperty(validationContext.DisplayName)!.SetValue(validationContext.ObjectInstance, targetValue);

        return ValidationResult.Success;
    }
}