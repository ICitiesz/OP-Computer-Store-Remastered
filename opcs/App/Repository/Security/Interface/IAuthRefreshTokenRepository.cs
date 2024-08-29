using opcs.App.Entity.Security;

namespace opcs.App.Repository.Security.Interface;

public interface IAuthRefreshTokenRepository
{
    Task<AuthRefreshToken> AddRefreshTokenAsync(AuthRefreshToken authRefreshToken);

    Task<AuthRefreshToken?> GetRefreshTokenByUserIdAsync(string userId);

    Task<AuthRefreshToken> UpdateRefreshToken(AuthRefreshToken authRefreshToken);

    bool HasRefreshTokenByUserId(string userId);

    Task<bool> RevokeRefreshTokenAsync(string userId);
}