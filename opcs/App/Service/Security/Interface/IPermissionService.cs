using opcs.App.Data.Dto.General;

namespace opcs.App.Service.Security.Interface;

public interface IPermissionService
{
    List<PermissionDto> GetAllPermissions();
}