using System.ComponentModel.DataAnnotations;
using opcs.App.Data.Dto.Security.Permission;

namespace opcs.App.Data.Dto.Security.Role;

public record UpdateRolePermissionRequestDto(
    [Required] long RoleId,
    [Required] List<RolePermissionDto> GrantedPermissions,
    [Required] List<RolePermissionDto> RevokedPermissions
);