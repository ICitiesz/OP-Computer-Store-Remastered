using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Entity;

public abstract class AuditBase
{
    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("created_by", TypeName = "varchar(15)")]
    public string CreatedBy { get; set; } = "SYSTEM";

    [Column("modified_at", TypeName = "datetime")]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    [Column("modified_by", TypeName = "varchar(15)")]
    public string ModifiedBy { get; set; } = "SYSTEM";
}