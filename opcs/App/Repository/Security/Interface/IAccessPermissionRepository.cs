using opcs.App.Entity.Security;

namespace opcs.App.Repository.Security.Interface;

public interface IAccessPermissionRepository
{
    Task<bool> AddAccessPermission(List<AccessPermission> permissions);

    Task<bool> RemoveAccessPermission(List<AccessPermission> permissions);

    bool HasPermissionByNameRoleId(string permission, long roleId);

    Task<List<AccessPermission>> GetRolePermissionsByRoleId(long roleId);
}