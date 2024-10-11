namespace opcs.App.Data.Dto.Security.Auth;

public record RefreshTokenDto(
    long TokenId,
    string Token,
    string UserId,
    DateTime TokenExpiry
);