using Microsoft.AspNetCore.Mvc.Filters;
using opcs.App.Data;

namespace opcs.App.Common.Attribute.Security;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiredClaim(string claimName, string claimValue) : System.Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.HasClaim(claimName, claimValue)) return;

        context.Result = new Response(
                statusCode: StatusCodes.Status403Forbidden,
                message: "You are not able to perform this action!"
            );
    }
}