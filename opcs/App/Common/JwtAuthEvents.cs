using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Service.Security.Interface;

namespace opcs.App.Common;

public class JwtAuthEvents : JwtBearerEvents
{
    public override Task TokenValidated(TokenValidatedContext context)
    {
        var reqUrl = context.Request.Path.Value!;

        if (reqUrl.Equals("/auth/register") || reqUrl.Equals("/auth/login"))
        {
            return Task.CompletedTask;
        }

        var securityService = context.HttpContext.RequestServices.GetAutofacRoot().Resolve<ISecurityService>();
        var claimPrincipal = context.Principal!;

        var userId = claimPrincipal.Claims
            .Find(claim => claim.Type is "userId")
            .Select(claim => claim.Value)
            .First();

        if (securityService.HasSession(userId)) return Task.CompletedTask;

        context.Fail("Unauthorized");

        return Task.CompletedTask;
    }
}