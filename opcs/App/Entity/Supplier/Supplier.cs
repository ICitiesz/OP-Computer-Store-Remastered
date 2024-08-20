using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Model.Supplier;

[Table("t_supplier")]
public class Supplier
{
    [Column("supplier_id", TypeName = "bigint")]
    public long SupplierId { get; set; }

    [Column("supplier_name", TypeName = "varchar(255)")]
    public string SupplierName { get; set; } = string.Empty;
}