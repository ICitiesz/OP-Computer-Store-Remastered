using LanguageExt;
using opcs.App.Data.Dto;
using opcs.App.Data.Dto.General;
using opcs.App.Data.Mapper;
using opcs.App.Repository.Supplier.Interface;
using opcs.App.Service.Supplier.Interface;

namespace opcs.App.Service.Supplier;

public class SupplierService(ISupplierRepository supplierRepository) : ISupplierService
{
    public List<SupplierDto> GetAllSupplier()
    {
        var suppliers = supplierRepository.GetAllAsync().Result;

        return ObjectMapper.GetMapper().Map<List<SupplierDto>>(suppliers);
    }

    public Option<SupplierDto> GetSupplierById(int supplierId)
    {
        var supplier = supplierRepository.GetByIdAsync(supplierId).Result;

        return ObjectMapper.GetMapper().Map<SupplierDto>(supplier);
    }
}