using opcs.App.Common.Attribute.Security;
using opcs.App.Database;
using opcs.App.Repository.Security.Interface;

namespace opcs.App.Repository.Security;

public class AccessPermissionRepository(AppDbContext dbContext) : IAccessPermissionRepository
{
    public bool HasPermissionByNameRoleId(string permission, long roleId)
    {
        return dbContext.AccessPermissions.Exists(accessPermission =>
            accessPermission.Permission == permission && accessPermission.RoleId == roleId);
    }
}