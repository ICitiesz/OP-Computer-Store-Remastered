using opcs.App.Common.Attribute.Security;
using opcs.App.Repository.Security.Interface;
using opcs.App.Service.Security.Interface;

namespace opcs.App.Service.Security;

public class AccessPermissionService(IAccessPermissionRepository accessPermissionRepository) : IAccessPermissionService
{
    public bool HasPermissionByNameRoleId(string permission, long roleId)
    {
        return accessPermissionRepository.HasPermissionByNameRoleId(permission, roleId);
    }
}