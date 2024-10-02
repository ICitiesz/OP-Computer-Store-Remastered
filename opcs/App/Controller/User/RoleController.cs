using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Data.Dto.Request;
using opcs.App.Service.User.Interface;
using opcs.Resources;

namespace opcs.App.Controller.User;

[Route("role")]
[Authorize]
[ApiController]
[Produces("application/json")]
public class RoleController(
    IRoleService roleService,
    ILogger<RoleController> logger) : ControllerBase
{
    [HttpGet("getAll")]
    [AllowAnonymous]
    public IActionResult GetAllRoles()
    {
        return new Response { dto = roleService.GetAllRole() };
    }

    [HttpGet]
    public IActionResult GetRoleById([FromQuery] int id)
    {
        return roleService.GetRoleById(id).Match(
            result => new Response { dto = result },
            () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_role_not_exist
            });
    }

    [HttpPost("updatePermission")]
    public IActionResult UpdateRolePermission([FromBody] UpdateRolePermissionRequestDto requestDto)
    {
        logger.LogInformation($"Req: {requestDto}");

        return new Response();
    }
}