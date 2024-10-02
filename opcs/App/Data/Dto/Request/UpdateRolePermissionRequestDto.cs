using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Request;

public record UpdateRolePermissionRequestDto(
    [Required] long RoleId,
    [Required] List<string> Permission
);