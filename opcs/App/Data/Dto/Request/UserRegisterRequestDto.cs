using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using opcs.App.Common.Attribute.Assertion;
using opcs.App.Data.Validation.Assertion;

namespace opcs.App.Data.Dto.Request;

public class UserRegisterRequestDto
{
    [Required]
    [MinLength(3), MaxLength(32)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText, MinLength(8), MaxLength(16), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText, MinLength(8), MaxLength(16), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$")]
    [AssertStringEqual("Password", errorMessage: "Confirm password is not equal!")]
    public string ConfirmPassword { get; set; } = string.Empty;
}