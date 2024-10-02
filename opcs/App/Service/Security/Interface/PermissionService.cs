using opcs.App.Core.Security;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Mapper;

namespace opcs.App.Service.Security.Interface;

public class PermissionService : IPermissionService
{
    public List<PermissionDto> GetAllPermissions()
    {
        return ObjectMapper.GetMapper().Map<List<PermissionDto>>(Permission.GetPermissionsInString());
    }
}