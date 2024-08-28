using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace opcs.App.Entity.Security;

[Table("t_role")]
[PrimaryKey("RoleId")]
public class Role : AuditBase
{
    [Column("role_id", TypeName = "bigint")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long RoleId { get; set; }

    [Column("role_name", TypeName = "varchar(64)")]
    public string RoleName { get; set; } = string.Empty;
}