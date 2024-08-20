using LanguageExt;
using opcs.App.Data.Dto;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Mapper;
using opcs.App.Repository.User.Interface;
using opcs.App.Service.User.Interface;

namespace opcs.App.Service.User;

public class RoleService(IRoleRepository iRoleRepository) : IRoleService
{
    public List<RoleDto> GetAllRole()
    {
        var roles = iRoleRepository.GetAllRolesAsync().Result;

        return ObjectMapper.GetMapper().Map<List<RoleDto>>(roles);
    }

    public Option<RoleDto> GetRoleById(int id)
    {
        var role = iRoleRepository.GetRoleByIdAsync(id).Result;

        return ObjectMapper.GetMapper().Map<RoleDto>(role);
    }
}