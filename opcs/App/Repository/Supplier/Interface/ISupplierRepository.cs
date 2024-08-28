namespace opcs.App.Repository.Supplier.Interface;

public interface ISupplierRepository
{
    Task<List<Entity.Supplier.Supplier>> GetAllAsync();

    Task<Entity.Supplier.Supplier?> GetByIdAsync(long id);
}