using System.ComponentModel.DataAnnotations;

namespace opcs.App.Data.Dto.Pagination.Sort;

public record QueryRoleSort
(
    [Required]
    bool RoleNameDesc = false
);