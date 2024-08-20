namespace opcs.App.Data.Dto.General;

public record SupplierDto
{
    public long SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
}