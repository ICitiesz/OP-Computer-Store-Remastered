using System.ComponentModel.DataAnnotations;
using opcs.Resources;

namespace opcs.App.Core.Attribute;

[AttributeUsage(AttributeTargets.Property)]
public class AssertEqual(string compareFieldName, string? errorMessage = null) : ValidationAttribute
{
    private readonly string _errorMessage = errorMessage ?? CodeMessages.opcs_error_request_value_not_equal;

    protected override ValidationResult? IsValid(object? currentFieldValue, ValidationContext validationContext)
    {
        if (currentFieldValue == null)
        {
            return new ValidationResult(CodeMessages.opcs_error_request_empty_property);
        }

        var compareField  = validationContext.ObjectType.GetProperty(compareFieldName);

        if (compareField == null)
        {
            return new ValidationResult(CodeMessages.opcs_error_request_invalid_property);
        }

        var fieldValue = compareField.GetValue(validationContext.ObjectInstance);

        return fieldValue == currentFieldValue ? ValidationResult.Success : new ValidationResult(_errorMessage);
    }
}