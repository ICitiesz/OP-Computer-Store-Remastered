using opcs.App.Data.Dto.General;

namespace opcs.App.Data.Dto.Response;

public class AuthenticationResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime ExpiredAt { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
}