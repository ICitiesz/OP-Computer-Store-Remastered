namespace opcs.App.Data.Dto.Security.Auth;

public record AuthenticationResponseDto(
    string AccessToken,
    string RefreshToken
);