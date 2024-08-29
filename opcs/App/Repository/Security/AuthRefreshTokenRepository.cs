using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Database;
using opcs.App.Entity.Security;
using opcs.App.Repository.Security.Interface;

namespace opcs.App.Repository.Security;

public class AuthRefreshTokenRepository(AppDbContext dbContext) : IAuthRefreshTokenRepository
{
    public async Task<AuthRefreshToken> AddRefreshTokenAsync(AuthRefreshToken authRefreshToken)
    {
        var result =  await dbContext.AuthRefreshTokens.AddAsync(authRefreshToken);

        await dbContext.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<AuthRefreshToken?> GetRefreshTokenByUserIdAsync(string userId)
    {
        var refreshToken = await dbContext.AuthRefreshTokens.FromSqlInterpolated(
            $"""
             SELECT *                            
             FROM t_auth_refresh_token tAuthRefreshToken
             WHERE tAuthRefreshToken.user_id = {userId} LIMIT 1
             """).ToArrayAsync();

        await dbContext.SaveChangesAsync();

        return !refreshToken.IsNullOrEmpty() ? refreshToken.First() : null;
    }

    public async Task<AuthRefreshToken> UpdateRefreshToken(AuthRefreshToken authRefreshToken)
    {
        var result = dbContext.AuthRefreshTokens.Update(authRefreshToken).Entity;

        await dbContext.SaveChangesAsync();

        return result;
    }

    public bool HasRefreshTokenByUserId(string userId)
    {
        return dbContext.AuthRefreshTokens.Exists(entity => entity.UserId == userId);
    }

    public async Task<bool> RevokeRefreshTokenAsync(string userId)
    {
        await dbContext.AuthRefreshTokens
            .Where(token => token.UserId == userId)
            .ExecuteDeleteAsync();

        return true;
    }
}