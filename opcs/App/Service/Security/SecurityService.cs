using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Common;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.Security.Interface;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace opcs.App.Service.Security;

public class SecurityService(IUserRepository userRepository, AppConfiguration appConfig) : ISecurityService
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
    /// Generate userId with ULID.
    /// </summary>
    /// <returns>An ULID as userId.</returns>
    public string GenerateUserId()
    {
        var userId = Ulid.NewUlid().ToString();

        while (userRepository.IsUserExistByUuidChar(userId))
        {
            userId = Ulid.NewUlid().ToString();
        }

        return userId;
    }

    public string GenerateToken(Entity.User.User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Name, user.Username),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.NameId, user.UserId)
        };

        var signingCredential = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = signingCredential,
            Issuer = appConfig.GetTokenIssuer(),
            Audience = appConfig.GetTokenAudience()
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}