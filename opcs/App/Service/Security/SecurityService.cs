using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Core;
using opcs.App.Core.Security;
using opcs.App.Data.Dto.General;
using opcs.App.Entity.Security;
using opcs.App.Repository.Security.Interface;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.Security.Interface;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace opcs.App.Service.Security;

public class SecurityService(
    IUserRepository userRepository,
    IAuthRefreshTokenRepository refreshTokenRepository,
    AppConfiguration appConfig) : ISecurityService
{
    private readonly PasswordHasher<Entity.User.User> _passwordHasher = new();
    private readonly SymmetricSecurityKey _securityKey = new(appConfig.GetTokenSigningKey());

    public string HashPassword(Entity.User.User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyHashedPassword(Entity.User.User user, string providedPassword, string hashedPassword)
    {
        var verifyResult = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);

        return verifyResult == PasswordVerificationResult.Success;
    }

    /// <summary>
    ///     Generate userId with ULID.
    /// </summary>
    /// <returns>An ULID as userId.</returns>
    public string GenerateUserId()
    {
        var userId = Ulid.NewUlid().ToString();

        while (userRepository.HasUserByUserId(userId)) userId = Ulid.NewUlid().ToString();

        return userId;
    }

    public string GenerateAccessToken(Entity.User.User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(JwtAuthClaims.Email, user.Email),
            new(JwtAuthClaims.UserId, user.UserId)
        };

        var signingCredential = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            // Expires = DateTime.UtcNow.AddMinutes(appConfig.GetAccessTokenLifeSpan()),
            Expires = DateTime.UtcNow.AddSeconds(7),
            SigningCredentials = signingCredential,
            Issuer = appConfig.GetTokenIssuer(),
            Audience = appConfig.GetTokenAudience()
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    public RefreshTokenDto GenerateUserRefreshToken(string userId, bool isRefreshAuth = false)
    {
        var userRefreshToken = refreshTokenRepository.GetRefreshTokenByUserIdAsync(userId).Result;
        const string creator = "SYSTEM";

        string tokenString;
        DateTime tokenExpiry;

        if (userRefreshToken is null)
        {
            tokenString = GenerateRefreshToken();
            tokenExpiry = DateTime.UtcNow.AddDays(appConfig.GetRefreshTokenLifeSpan());

            userRefreshToken = new AuthRefreshToken
            {
                RefreshToken = tokenString,
                UserId = userId,
                Expiry = tokenExpiry,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creator,
                ModifiedAt = DateTime.UtcNow,
                ModifiedBy = creator
            };

            userRefreshToken = refreshTokenRepository.AddRefreshTokenAsync(userRefreshToken).Result;
        }
        else
        {
            /* Check if it is refresh auth or refresh token expired */
            if (isRefreshAuth || IsRefreshTokenExpired(userRefreshToken.Expiry))
            {
                tokenString = GenerateRefreshToken();
                tokenExpiry = DateTime.UtcNow.AddDays(appConfig.GetRefreshTokenLifeSpan());

                userRefreshToken.RefreshToken = tokenString;
                userRefreshToken.Expiry = tokenExpiry;
                userRefreshToken.ModifiedAt = DateTime.UtcNow;
                userRefreshToken.ModifiedBy = creator;

                userRefreshToken = refreshTokenRepository.UpdateRefreshToken(userRefreshToken).Result;
            }

            tokenString = userRefreshToken.RefreshToken;
            tokenExpiry = userRefreshToken.Expiry;
        }

        return new RefreshTokenDto
        (
            userRefreshToken.TokenId,
            tokenString,
            userId,
            tokenExpiry
        );
    }

    public (bool, Entity.User.User?) ValidateAccessToken(string expiredAccessToken)
    {
        var claimPrinciple = GetExpiredAccessTokenPrincipal(expiredAccessToken);

        if (claimPrinciple is null) return (false, null);

        var claims = claimPrinciple.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);

        if (!(claims.ContainsKey(JwtRegisteredClaimNames.Name)
              && claims.ContainsKey(JwtAuthClaims.Email)
              && claims.ContainsKey(JwtAuthClaims.UserId)))
            return (false, null);

        var userId = claims[JwtAuthClaims.UserId];
        var user = userRepository.GetUserByUserId(userId).Result;

        return user is not null ? (true, user) : (false, null);
    }

    public bool ValidateRefreshToken(string userId, string providedRefreshToken)
    {
        var userRefreshToken = refreshTokenRepository.GetRefreshTokenByUserIdAsync(userId).Result;

        if (userRefreshToken is null) return false;

        if (!userRefreshToken.RefreshToken.Equals(providedRefreshToken)) return false;

        return !IsRefreshTokenExpired(userRefreshToken.Expiry);
    }

    public bool HasSession(string userId)
    {
        return refreshTokenRepository.HasRefreshTokenByUserId(userId);
    }

    private static string GenerateRefreshToken()
    {
        var rngNumber = new byte[64];

        using var rngNumGenerator = RandomNumberGenerator.Create();

        rngNumGenerator.GetBytes(rngNumber);

        return Convert.ToBase64String(rngNumber);
    }

    public Option<RefreshTokenDto> GetUserRefreshToken(string userId)
    {
        var authRefreshToken = refreshTokenRepository.GetRefreshTokenByUserIdAsync(userId).Result;

        return authRefreshToken != null
            ? new RefreshTokenDto
            (
                authRefreshToken.TokenId,
                authRefreshToken.RefreshToken,
                authRefreshToken.UserId,
                authRefreshToken.Expiry
            )
            : null;
    }

    private static bool IsRefreshTokenExpired(DateTime tokenExpiry)
    {
        return tokenExpiry < DateTime.UtcNow;
    }

    private ClaimsPrincipal? GetExpiredAccessTokenPrincipal(string expiredAccessToken)
    {
        try
        {
            return new JwtSecurityTokenHandler().ValidateToken(expiredAccessToken,
                appConfig.GetTokenValidationParameters(false), out _);
        }
        catch (SecurityTokenMalformedException err)
        {
            Console.WriteLine(err);
            return null;
        }
    }
}