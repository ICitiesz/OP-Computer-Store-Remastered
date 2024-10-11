namespace opcs.App.Data.Dto.Security.Role;

public record RoleDto(
    long RoleId,
    string RoleName,
    DateTime CreatedDate
);