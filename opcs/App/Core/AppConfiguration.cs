using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace opcs.App.Core;

public class AppConfiguration(IConfiguration configuration)
{
    public PathString GetBasePath()
    {
        return new PathString(configuration["BasePath"]);
    }

    public string GetTokenIssuer()
    {
        return configuration["JWT:Issuer"]!;
    }

    public string GetTokenAudience()
    {
        return configuration["JWT:Audience"]!;
    }

    public byte[] GetTokenSigningKey()
    {
        return Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]!);
    }

    /// <summary>
    /// Get the access token life span in minutes.
    /// </summary>
    /// <returns>Life span in minutes.</returns>
    public int GetAccessTokenLifeSpan()
    {
        return configuration.GetValue<int>("JWT:AccessTokenLifeSpanMinutes");
    }

    /// <summary>
    /// Get the refresh token life span in days.
    /// </summary>
    /// <returns>Life span in days.</returns>
    public int GetRefreshTokenLifeSpan()
    {
        return configuration.GetValue<int>("JWT:RefreshTokenLifeSpanDays");
    }

    public int GetAccessTokenClockSkew()
    {
        return configuration.GetValue<int>("JWT:AccessTokenClockSkewSeconds");
    }

    public TokenValidationParameters GetTokenValidationParameters(bool init = true)
    {
        var issuerSigningKey = new SymmetricSecurityKey(GetTokenSigningKey());

        return init
            ? new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = GetTokenIssuer(),
                ValidAudience = GetTokenAudience(),
                IssuerSigningKey = issuerSigningKey,
                ClockSkew = TimeSpan.FromSeconds(5)
            }
            : new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = GetTokenIssuer(),
                ValidAudience = GetTokenAudience(),
                IssuerSigningKey = issuerSigningKey
            };
    }
}