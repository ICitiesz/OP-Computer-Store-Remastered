using System.ComponentModel.DataAnnotations;
using opcs.Resources;

namespace opcs.App.Common.Attribute.Assertion;

[AttributeUsage(AttributeTargets.Property)]
public class AssertNotNullOrEmpty : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
                return new ValidationResult(CodeMessages.opcs_error_request_empty_property);

            case string val:
            {
                if (!string.IsNullOrEmpty(val) && !string.IsNullOrWhiteSpace(val)) break;

                return new ValidationResult(CodeMessages.opcs_error_request_empty_property);
            }
        }

        return ValidationResult.Success;
    }
}