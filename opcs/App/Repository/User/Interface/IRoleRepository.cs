using opcs.App.Model.User;

namespace opcs.App.Repository.User.Interface;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();

    Task<Role?> GetRoleByIdAsync(long roleId);
}