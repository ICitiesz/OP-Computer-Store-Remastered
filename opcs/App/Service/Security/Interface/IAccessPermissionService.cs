namespace opcs.App.Service.Security.Interface;

public interface IAccessPermissionService
{
    bool HasPermissionByNameRoleId(string permission, long roleId);
}