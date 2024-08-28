namespace opcs.App.Repository.Security.Interface;

public interface IAccessPermissionRepository
{
    bool HasPermissionByNameRoleId(string permission, long roleId);
}