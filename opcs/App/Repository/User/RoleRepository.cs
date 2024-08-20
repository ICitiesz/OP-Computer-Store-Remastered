using Microsoft.EntityFrameworkCore;
using opcs.App.Database;
using opcs.App.Model.User;
using opcs.App.Repository.User.Interface;

namespace opcs.App.Repository.User;

public class RoleRepository(AppDbContext appDbContext) : IRoleRepository
{
    public async Task<List<Role>> GetAllRolesAsync()
    {
        return await appDbContext.Role.AsNoTracking().ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(long roleId)
    {
        return await appDbContext.Role.FindAsync(roleId);
    }
}