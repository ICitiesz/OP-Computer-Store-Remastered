using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Service.Security.Interface;
using opcs.Resources;

namespace opcs.App.Core.Security;

public class JwtAuthEvents : JwtBearerEvents
{
    public override Task TokenValidated(TokenValidatedContext context)
    {
        var reqUrl = context.Request.Path.Value!;

        if (reqUrl.Equals("/auth/register") || reqUrl.Equals("/auth/login")) return Task.CompletedTask;

        var securityService = context.HttpContext.RequestServices.GetAutofacRoot().Resolve<ISecurityService>();
        var claimPrincipal = context.Principal!;

        var userId = claimPrincipal.Claims
            .Find(claim => claim.Type is JwtAuthClaims.UserId)
            .Select(claim => claim.Value)
            .First();

        if (securityService.HasSession(userId)) return Task.CompletedTask;

        context.Fail(CodeMessages.opcs_error_auth_unauthorized);

        return Task.CompletedTask;
    }

    public override Task MessageReceived(MessageReceivedContext context)
    {
        if (context.Request.Cookies.IsNullOrEmpty()
            || !context.Request.Cookies.ContainsKey("accessToken")
            || !context.Request.Cookies.ContainsKey("refreshToken"))
        {
            context.Fail(CodeMessages.opcs_error_auth_unauthorized);
            return Task.CompletedTask;
        }

        context.Token = context.Request.Cookies["accessToken"];
        return Task.CompletedTask;
    }
}