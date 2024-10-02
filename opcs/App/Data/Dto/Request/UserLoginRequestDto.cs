using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Request;

public record UserLoginRequestDto(
    [Required] string UsernameOrEmail,
    [Required] [PasswordPropertyText] string Password
);