using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Entity;

public abstract class AuditBase
{
    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("created_by", TypeName = "varchar(15)")]
    public string CreatedBy { get; set; } = string.Empty;

    [Column("modified_at", TypeName = "datetime")]
    public DateTime ModifiedAt { get; set; }

    [Column("modified_by", TypeName = "varchar(15)")]
    public string ModifiedBy { get; set; } = string.Empty;
}