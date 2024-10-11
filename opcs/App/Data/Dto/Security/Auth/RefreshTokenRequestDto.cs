using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Security.Auth;

public record RefreshTokenRequestDto(
    [Required] string AccessToken,
    [Required] string RefreshToken
);