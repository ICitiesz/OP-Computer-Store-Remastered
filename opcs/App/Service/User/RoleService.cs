using LanguageExt;
using opcs.App.Data.Dto;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Mapper;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.User.Interface;

namespace opcs.App.Service.User;

public class RoleService(IRoleRepository roleRepository) : IRoleService
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
        var result =  roleRepository.GetUserRole(userId).Result;

        return result is not null ? ObjectMapper.GetMapper().Map<RoleDto>(result) : new Option<RoleDto>();
    }
}