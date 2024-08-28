using System.ComponentModel.DataAnnotations;
using opcs.App.Common.Attribute.Assertion;

namespace opcs.App.Data.Dto.Request;

public record RefreshTokenRequestDto
(
    [Required]
    string AccessToken,

    [Required]
    string RefreshToken
);