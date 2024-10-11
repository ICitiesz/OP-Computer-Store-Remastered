using LanguageExt;
using opcs.App.Data.Dto.Supplier;

namespace opcs.App.Service.Supplier.Interface;

public interface ISupplierService
{
    List<SupplierDto> GetAllSupplier();

    Option<SupplierDto> GetSupplierById(int supplierId);
}