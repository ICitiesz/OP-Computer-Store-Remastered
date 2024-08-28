using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Request;

public record RefreshTokenRequestDto
(
    [Required]
    string AccessToken,

    [Required]
    string RefreshToken
);