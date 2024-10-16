using LanguageExt;
using opcs.App.Data.Dto.Pagination;
using opcs.App.Data.Dto.Pagination.Search;
using opcs.App.Data.Dto.Security.Role;

namespace opcs.App.Service.User.Interface;

public interface IRoleService
{
    List<RoleDto> GetAllRole();

    Option<RoleDto> GetRoleById(int id);

    bool HasUserRole(string userId);

    Option<RoleDto> GetUserRole(string userId);

    PaginationResponseDto<RoleDto> QueryRole(PaginationRequestDto<QueryRoleSearch> requestDto);
}