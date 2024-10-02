namespace opcs.App.Data.Dto.General;

public record RefreshTokenDto(
    long TokenId,
    string Token,
    string UserId,
    DateTime TokenExpiry
);