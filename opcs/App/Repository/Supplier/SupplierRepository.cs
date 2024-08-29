using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using opcs.App.Database;
using opcs.App.Repository.Supplier.Interface;

namespace opcs.App.Repository.Supplier;

public class SupplierRepository(AppDbContext appDbContext) : ISupplierRepository
{
    public async Task<List<Entity.Supplier.Supplier>> GetAllAsync()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        var suppliers = await appDbContext.Supplier.AsNoTracking().ToListAsync();

        stopWatch.Stop();

        Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds} ms");

        return suppliers;
        //return await appDbContext.Supplier.ToListAsync();
    }

    public async Task<Entity.Supplier.Supplier?> GetByIdAsync(long id)
    {
        return await appDbContext.Supplier.FindAsync(id);
    }
}