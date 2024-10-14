using LanguageExt;
using opcs.App.Data.Dto.Pagination;
using opcs.App.Data.Dto.Pagination.Search;
using opcs.App.Data.Dto.Pagination.Sort;
using opcs.App.Data.Dto.Security.Permission;
using opcs.App.Data.Dto.Security.Role;
using opcs.App.Data.Mapper;
using opcs.App.Repository.Security.Interface;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.User.Interface;

namespace opcs.App.Service.User;

public class RoleService(
    IRoleRepository roleRepository
) : IRoleService
{
    public List<RoleDto> GetAllRole()
    {
        var roles = roleRepository.GetAllRolesAsync().Result;

        return ObjectMapper.GetMapper().Map<List<RoleDto>>(roles);
    }

    public Option<RoleDto> GetRoleById(int id)
    {
        var role = roleRepository.GetRoleByIdAsync(id).Result;

        return ObjectMapper.GetMapper().Map<RoleDto>(role);
    }

    public bool HasUserRole(string userId)
    {
        return roleRepository.HasUserRole(userId).Result;
    }

    public Option<RoleDto> GetUserRole(string userId)
    {
        var userRole = roleRepository.GetUserRole(userId).Result;

        return userRole is not null ? ObjectMapper.GetMapper().Map<RoleDto>(userRole) : new Option<RoleDto>();
    }

    public PaginationResponseDto<RoleDto> QueryRole(PaginationRequestDto<QueryRoleSearch, QueryRoleSort> requestDto)
    {
        var roles = roleRepository.QueryRole(requestDto).Result;
        var remappedRoles = ObjectMapper.GetMapper().Map<List<RoleDto>>(roles.Item1);
        var totalPages = GetTotalPages(roles.Item2, requestDto.TotalItemsPerPage);

        return new PaginationResponseDto<RoleDto>
        (
            CurrentPage: requestDto.CurrentPage > totalPages ? totalPages : requestDto.CurrentPage,
            TotalItems: roles.Item2,
            TotalPages: totalPages,
            Items: remappedRoles
        );
    }

    private static int GetTotalPages(int itemsCount, int totalItemsCount)
    {
        return Convert.ToInt32(Math.Ceiling((double) itemsCount / totalItemsCount));
    }
}