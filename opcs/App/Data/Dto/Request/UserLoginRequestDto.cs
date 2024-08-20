using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Request;

public class UserLoginRequestDto
{
    [Required]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
}