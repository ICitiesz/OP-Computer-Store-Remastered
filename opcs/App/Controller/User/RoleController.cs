using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Core.Attribute;
using opcs.App.Core.Security;
using opcs.App.Data;
using opcs.App.Service.User.Interface;
using opcs.Resources;
using AppContext = opcs.App.Core.AppContext;

namespace opcs.App.Controller.User;

[Route("role")]
[Authorize]
[ApiController, Produces("application/json")]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [RequiredPermission(Permission.ManageUser)]
    [HttpGet("getAll")]
    public IActionResult GetAllRoles()
    {
        return new Response { dto = roleService.GetAllRole()};
    }

    [HttpGet]
    public IActionResult GetRoleById([FromQuery] int id)
    {
        return roleService.GetRoleById(id).Match(
            Some: result => new Response { dto = result},
            None: () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_role_not_exist
            });
    }
}