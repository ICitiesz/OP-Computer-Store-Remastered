using opcs.App.Data.Dto.Security.Auth;

namespace opcs.App.Service.Security.Interface;

public interface ISecurityService
{
    string HashPassword(Entity.User.User user, string password);

    bool VerifyHashedPassword(Entity.User.User user, string providedPassword, string hashedPassword);

    string GenerateUserId();

    string GenerateAccessToken(Entity.User.User user);

    RefreshTokenDto GenerateUserRefreshToken(string userId, bool isRefreshAuth = false);

    (bool, Entity.User.User?) ValidateAccessToken(string expiredAccessToken);

    bool ValidateRefreshToken(string userId, string providedRefreshToken);

    bool HasSession(string userId);
}