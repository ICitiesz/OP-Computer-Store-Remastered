using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using opcs.App.Core.Security;
using opcs.App.Data.Dto.Security.Permission;
using opcs.App.Data.Dto.Security.Role;
using opcs.App.Data.Mapper;
using opcs.App.Entity.Security;
using opcs.App.Repository.Security.Interface;
using opcs.App.Repository.User.Interface;

namespace opcs.App.Service.Security.Interface;

public class PermissionService(
    IHttpContextAccessor httpContextAccessor,

    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IAccessPermissionRepository accessPermissionRepository
) : IPermissionService
{
    public List<PermissionDto> GetAllPermissions()
    {
        return ObjectMapper.GetMapper().Map<List<PermissionDto>>(Permission.GetPermissionsInString());
    }

    public List<RolePermissionDto> GetRolePermissionsByRoleId(long roleId)
    {
        return !roleRepository.HasRoleByRoleId(roleId) ? []
            : ObjectMapper.GetMapper().Map<List<RolePermissionDto>>(accessPermissionRepository.GetRolePermissionsByRoleId(roleId).Result);
    }

    public bool UpdateRolePermissions(UpdateRolePermissionRequestDto requestDto)
    {
        var userId = httpContextAccessor.HttpContext!.User.Claims
            .Find(claim => claim.Type == JwtAuthClaims.UserId)
            .FirstOrDefault()
            ?.Value;

        if (userId == null) return false;

        var user = userRepository.GetUserByUserId(userId).Result;

        if (user == null) return false;

        if (!roleRepository.HasRoleByRoleId(requestDto.RoleId)) return false;

        var rolePermissions = accessPermissionRepository.GetRolePermissionsByRoleId(requestDto.RoleId).Result;
        var grantedRolePermissions = new List<AccessPermission>();
        var revokedRolePermissions = new List<AccessPermission>();

        if (!requestDto.GrantedPermissions.IsNullOrEmpty())
        {
            grantedRolePermissions = requestDto.GrantedPermissions
                .Filter(grantedRolePermission => !rolePermissions
                    .Select(rolePermission => new {rolePermission.Permission, rolePermission.RoleId})
                    .Any(rolePermission =>
                        rolePermission.RoleId == requestDto.RoleId && rolePermission.Permission == grantedRolePermission.Permission
                    ))
                .Select(grantedPermission => new AccessPermission
                {
                    Permission = grantedPermission.Permission,
                    RoleId = requestDto.RoleId,
                    CreatedBy = user.Username,
                    ModifiedBy = user.Username
                }).ToList();
        }

        if (!requestDto.RevokedPermissions.IsNullOrEmpty())
        {
            if (!rolePermissions.IsNullOrEmpty())
            {
                 var revokedRolePermissionIds = requestDto.RevokedPermissions
                     .Select(revokedRolePermission => revokedRolePermission.Id).ToList();

                revokedRolePermissions = rolePermissions
                    .Where(rolePermission => revokedRolePermissionIds.Contains(rolePermission.Id))
                    .ToList();
            }
        }

        if (!grantedRolePermissions.IsNullOrEmpty())
        {
            var granted = accessPermissionRepository.AddAccessPermission(grantedRolePermissions).Result;
        }

        if (!revokedRolePermissions.IsNullOrEmpty())
        {
            var removed = accessPermissionRepository.RemoveAccessPermission(revokedRolePermissions).Result;
        }

        return true;
    }
}