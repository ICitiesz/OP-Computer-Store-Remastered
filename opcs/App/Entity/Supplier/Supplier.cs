using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Entity.Supplier;

[Table("t_supplier")]
public class Supplier : AuditBase
{
    [Column("supplier_id", TypeName = "bigint")]
    public long SupplierId { get; set; }

    [Column("supplier_name", TypeName = "varchar(255)")]
    public string SupplierName { get; set; } = string.Empty;
}