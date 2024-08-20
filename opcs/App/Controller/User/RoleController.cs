using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Service.User.Interface;
using AppContext = opcs.App.Common.AppContext;

namespace opcs.App.Controller.User;

[Route("role")]
[Authorize]
[ApiController, Produces("application/json")]
public class RoleController(IRoleService iRoleService) : ControllerBase
{
    [HttpGet("getAll")]
    public IActionResult GetAllRoles()
    {
        return new Response(iRoleService.GetAllRole());
    }

    [HttpGet]
    public IActionResult GetRoleById([FromQuery] int id)
    {
        return iRoleService.GetRoleById(id).Match(
            Some: result => new Response(result),
            None: () => new Response(
                null,
                statusCode: StatusCodes.Status400BadRequest,
                message: AppContext.GetCodeMessage("opcs.error.role.not_exist")
            ));
    }
}