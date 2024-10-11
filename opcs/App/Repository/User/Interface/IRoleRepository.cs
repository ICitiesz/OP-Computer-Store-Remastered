using opcs.App.Data.Dto.Pagination;
using opcs.App.Data.Dto.Pagination.Sort;
using opcs.App.Entity.Security;

namespace opcs.App.Repository.User.Interface;

public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();

    Task<Role?> GetRoleByIdAsync(long roleId);

    bool HasRoleByRoleId(long roleId);

    Task<bool> HasUserRole(string userId);

    Task<Role?> GetUserRole(string userId);

    Task<(List<Role>, int)> QueryRole(PaginationRequestDto<object, QueryRoleSort> requestDto);
}