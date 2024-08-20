using System.Text;

namespace opcs.App.Common;

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
}