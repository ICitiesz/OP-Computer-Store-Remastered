using System.ComponentModel.DataAnnotations;
using AppContext = opcs.App.Common.AppContext;

namespace opcs.App.Data.Validation.Assertion;

[AttributeUsage(AttributeTargets.Property)]
public class AssertEqual(string compareFieldName, string? errorMessage = null) : ValidationAttribute
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

        var fieldValue = compareField.GetValue(validationContext.ObjectInstance);

        return fieldValue == currentFieldValue ? ValidationResult.Success : new ValidationResult(_errorMessage);
    }
}