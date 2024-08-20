using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace opcs.App.Model.User;

[Table("t_role")]
[PrimaryKey("RoleId")]
public class Role
{
    [Column("role_id", TypeName = "bigint")]
    public long RoleId { get; set; }

    [Column("role_name", TypeName = "varchar(64)")]
    public string RoleName { get; set; } = string.Empty;
}