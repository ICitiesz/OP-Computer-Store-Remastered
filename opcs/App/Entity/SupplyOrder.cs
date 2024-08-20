using System.ComponentModel.DataAnnotations.Schema;

namespace opcs.App.Model;

[Table("t_supply_order")]
public class SupplyOrder
{
    [Column(TypeName = "bigint")] public long SupplyOrderId { get; set; }

    [Column(TypeName = "bigint")] public long? SupplierId { get; set; }

    public Supplier.Supplier? Supplier { get; set; }

    public DateOnly OrderDate { get; set; }

    [Column(TypeName = "varchar(20)")] public string OrderStatus { get; set; } = string.Empty;
}