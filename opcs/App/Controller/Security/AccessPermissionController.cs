using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Core.Attribute;
using opcs.App.Core.Security;
using opcs.App.Data;
using opcs.App.Data.Dto.Security.Role;
using opcs.App.Service.Security.Interface;

namespace opcs.App.Controller.Security;

[Route("permission")]
[Authorize, RequiredPermission(Permission.PermissionEnum.ManagePermission)]
[ApiController]
[Produces("application/json")]
public class AccessPermissionController(IPermissionService permissionService)
    : ControllerBase
{
    [HttpGet("getAll")]
    [AllowAnonymous]
    public IActionResult GetAllPermissions()
    {
        return new Response
        {
            dto = permissionService.GetAllPermissions()
        };
    }

    [HttpPost("updatePermission")]
    public IActionResult UpdateRolePermission([FromBody] UpdateRolePermissionRequestDto requestDto)
    {
        return new Response
        {
            dto = permissionService.UpdateRolePermissions(requestDto)
        };
    }

    [HttpGet("getPermissionByRoleId")]
    public IActionResult GetRolePermissionByRoleId([FromQuery, Required] int roleId)
    {
        return new Response
        {
            dto = permissionService.GetRolePermissionsByRoleId(roleId)
        };
    }
}