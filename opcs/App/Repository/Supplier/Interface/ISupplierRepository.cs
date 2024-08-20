namespace opcs.App.Repository.Supplier.Interface;

using opcs.App.Model.Supplier;

public interface ISupplierRepository
{
    Task<List<Supplier>> GetAllAsync();

    Task<Supplier?> GetByIdAsync(long id);
}