using opcs.App.Entity.Security;

namespace opcs.App.Repository.User.Interface;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();

    Task<Role?> GetRoleByIdAsync(long roleId);

    Task<bool> HasUserRole(string userId);

    Task<Role?> GetUserRole(string userId);
}