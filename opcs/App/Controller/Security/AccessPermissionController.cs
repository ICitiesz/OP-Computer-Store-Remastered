using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Data;
using opcs.App.Service.Security.Interface;

namespace opcs.App.Controller.Security;

[Route("permission")]
[Authorize]
[ApiController]
[Produces("application/json")]
public class AccessPermissionController(IPermissionService permissionService)
    : ControllerBase
{
    [HttpGet("getAll")]
    [AllowAnonymous]
    public IActionResult GetAllPermissions()
    {
        Console.WriteLine("Getting all permissions");
        return new Response
        {
            dto = permissionService.GetAllPermissions()
        };
    }
}