namespace opcs.App.Data.Dto.Response;

public record AuthenticationResponseDto(
    string AccessToken,
    string RefreshToken
);