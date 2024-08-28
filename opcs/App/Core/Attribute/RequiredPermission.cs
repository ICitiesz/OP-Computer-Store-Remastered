using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using opcs.App.Data;
using opcs.App.Service.Security.Interface;
using opcs.App.Service.User.Interface;
using opcs.Resources;

namespace opcs.App.Core.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class RequiredPermission(string permission) : System.Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var serviceProvider = context.HttpContext.RequestServices;
        var accessPermissionService = serviceProvider.GetAutofacRoot().Resolve<IAccessPermissionService>();
        var roleService = serviceProvider.GetAutofacRoot().Resolve<IRoleService>();

        var userId = context.HttpContext.User.Claims
            .FirstOrDefault(claim => claim.Type == "userId")?.Value;

        if (userId is null)
        {
            context.Result = new Response
            {
                statusCode = StatusCodes.Status401Unauthorized,
                message = CodeMessages.opcs_error_auth_unauthorized
            };
            return;
        }

        var role = roleService.GetUserRole(userId).SingleOrDefault();

        if (role is null || !accessPermissionService.HasPermissionByNameRoleId(permission, role!.RoleId))
        {
            context.Result = new Response
            {
                statusCode = StatusCodes.Status403Forbidden,
                message = CodeMessages.opcs_error_permission_forbidden_access
            };
        }
    }
}