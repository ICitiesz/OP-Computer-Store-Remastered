using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using opcs.App.Core.Attribute;
using opcs.App.Core.Security;
using opcs.App.Data;
using opcs.App.Data.Dto.Pagination;
using opcs.App.Data.Dto.Pagination.Search;
using opcs.App.Service.User.Interface;
using opcs.Resources;

namespace opcs.App.Controller.Security;

[Route("role")]
[Authorize]
[ApiController]
[Produces("application/json"), RequiredPermission(Permission.PermissionEnum.ManageRole)]
public class RoleController(
    IRoleService roleService,
    ILogger<RoleController> logger) : ControllerBase
{
    [HttpGet("getAll")]
    public IActionResult GetAllRoles()
    {
        return new Response { dto = roleService.GetAllRole() };
    }

    [HttpGet]
    public IActionResult GetRoleById([FromQuery, Required] int id)
    {
        return roleService.GetRoleById(id).Match(
            result => new Response { dto = result },
            () => new Response
            {
                statusCode = StatusCodes.Status400BadRequest,
                message = CodeMessages.opcs_error_role_not_exist
            });
    }

    [HttpPost("queryPage")]
    public IActionResult QueryRole(
        [FromBody, AssertSortField<QueryRoleSearch>(QuerySortField.SortFieldEnum.RoleSortField)]
        PaginationRequestDto<QueryRoleSearch> requestDto)
    {
        return new Response
        {
            dto = roleService.QueryRole(requestDto)
        };
    }
}