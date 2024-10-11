using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Security.Permission;

public record RolePermissionDto(
    [Required]
    long Id,

    [Required]
    string Permission,

    [Required]
    long RoleId
);