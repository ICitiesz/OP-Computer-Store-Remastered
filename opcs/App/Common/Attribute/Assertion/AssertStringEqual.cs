using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace opcs.App.Common.Attribute.Assertion;

[AttributeUsage(AttributeTargets.Property)]
public class AssertStringEqual(string compareFieldName, bool ignoreCase = false, string? errorMessage = null) : ValidationAttribute
{
    private readonly string _errorMessage = errorMessage ?? AppContext.GetCodeMessage("opcs.error.request.value_not_equal");

    protected override ValidationResult? IsValid(object? currentFieldValue, ValidationContext validationContext)
    {
        if (currentFieldValue == null)
        {
            return new ValidationResult(AppContext.GetCodeMessage("opcs.error.request.empty_property"));
        }

        var compareField  = validationContext.ObjectType.GetProperty(compareFieldName);

        if (compareField == null)
        {
            return new ValidationResult(AppContext.GetCodeMessage("opcs.error.request.no_such_property"));
        }

        if (compareField.PropertyType != typeof(string))
        {
            return new ValidationResult(AppContext.GetFormattedCodeMessage("opcs.error.request.invalid_property_type", "string"));
        }

        var compareFieldValue = compareField.GetValue(validationContext.ObjectInstance) as string;

        if (compareFieldValue.IsNullOrEmpty())
        {
            return new ValidationResult(AppContext.GetCodeMessage("opcs.error.request.empty_property"));
        }

        if (ignoreCase)
        {
            return string.Equals(compareFieldValue, currentFieldValue as string, StringComparison.OrdinalIgnoreCase)
                ? ValidationResult.Success : new ValidationResult(_errorMessage);
        }

        return string.Equals(compareFieldValue, currentFieldValue as string, StringComparison.Ordinal)
            ? ValidationResult.Success : new ValidationResult(_errorMessage);
    }
}