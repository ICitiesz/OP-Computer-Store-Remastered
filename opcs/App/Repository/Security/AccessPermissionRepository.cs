using Microsoft.EntityFrameworkCore;
using opcs.App.Database;
using opcs.App.Entity.Security;
using opcs.App.Repository.Security.Interface;

namespace opcs.App.Repository.Security;

public class AccessPermissionRepository(AppDbContext dbContext) : IAccessPermissionRepository
{
    public async Task<bool> AddAccessPermission(List<AccessPermission> permissions)
    {
        await dbContext.AccessPermissions.AddRangeAsync(permissions);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveAccessPermission(List<AccessPermission> permissions)
    {
        dbContext.AccessPermissions.RemoveRange(permissions);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public bool HasPermissionByNameRoleId(string permission, long roleId)
    {
        return dbContext.AccessPermissions.Exists(accessPermission =>
            accessPermission.Permission == permission && accessPermission.RoleId == roleId);
    }

    public async Task<List<AccessPermission>> GetRolePermissionsByRoleId(long roleId)
    {
        return await dbContext.AccessPermissions
            .Where(accessPermission => accessPermission.RoleId == roleId)
            .ToListAsync();
    }
}