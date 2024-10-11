using opcs.App.Data.Dto.Security.Permission;
using opcs.App.Data.Dto.Security.Role;

namespace opcs.App.Service.Security.Interface;

public interface IPermissionService
{
    List<PermissionDto> GetAllPermissions();

    List<RolePermissionDto> GetRolePermissionsByRoleId(long roleId);

    bool UpdateRolePermissions(UpdateRolePermissionRequestDto requestDto);
}