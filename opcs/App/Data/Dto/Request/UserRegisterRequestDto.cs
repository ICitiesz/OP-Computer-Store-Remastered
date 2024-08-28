using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using opcs.App.Common.Attribute.Assertion;
using opcs.Resources;

namespace opcs.App.Data.Dto.Request;

public record UserRegisterRequestDto
(
    [Required]
    [MinLength(3), MaxLength(32)]
    string Username,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [PasswordPropertyText, MinLength(8), MaxLength(16), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$")]
    string Password,

    [Required]
    [PasswordPropertyText, MinLength(8), MaxLength(16), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).*$")]
    [AssertStringEqual("Password", errorMessage: "Confirm password and password must be the same!")]
    string ConfirmPassword
);