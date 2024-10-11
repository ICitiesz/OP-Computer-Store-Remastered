using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Security.Auth;

public record UserLoginRequestDto(
    [Required] string UsernameOrEmail,
    [Required] [PasswordPropertyText] string Password
);