using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace opcs.App.Entity.Security;

[Table("t_access_permission")]
public class AccessPermission : AuditBase
{
    [Column("id", TypeName = "bigint")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("permission", TypeName = "varchar(128)")]
    public string Permission { get; set; } = string.Empty;

    [Column("role_id", TypeName = "bigint")]
    public long RoleId { get; set; }

    [ForeignKey("RoleId")] public Role Role { get; set; }
}