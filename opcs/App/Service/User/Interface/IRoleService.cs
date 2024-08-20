using LanguageExt;
using opcs.App.Data.Dto;
using opcs.App.Data.Dto.General;

namespace opcs.App.Service.User.Interface;

public interface IRoleService
{
    List<RoleDto> GetAllRole();

    Option<RoleDto> GetRoleById(int id);
}