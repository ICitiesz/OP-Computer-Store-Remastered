using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using opcs.Resources;

namespace opcs.App.Core.Attribute;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public class AssertStringEqual(string compareFieldName, bool ignoreCase = false, string? errorMessage = null)
    : ValidationAttribute
{
    private readonly string _errorMessage = errorMessage ?? CodeMessages.opcs_error_request_value_not_equal;

    protected override ValidationResult? IsValid(object? currentFieldValue, ValidationContext validationContext)
    {
        if (currentFieldValue == null) return new ValidationResult(CodeMessages.opcs_error_request_empty_property);

        var compareField = validationContext.ObjectType.GetProperty(compareFieldName);

        if (compareField == null) return new ValidationResult(CodeMessages.opcs_error_request_invalid_property);

        if (compareField.PropertyType != typeof(string))
            return new ValidationResult(string.Format(CodeMessages.opcs_error_request_invalid_property_type, "string"));

        var compareFieldValue = compareField.GetValue(validationContext.ObjectInstance) as string;

        if (compareFieldValue.IsNullOrEmpty())
            return new ValidationResult(CodeMessages.opcs_error_request_empty_property);

        if (ignoreCase)
            return string.Equals(compareFieldValue, currentFieldValue as string, StringComparison.OrdinalIgnoreCase)
                ? ValidationResult.Success
                : new ValidationResult(_errorMessage);

        return string.Equals(compareFieldValue, currentFieldValue as string, StringComparison.Ordinal)
            ? ValidationResult.Success
            : new ValidationResult(_errorMessage);
    }
}