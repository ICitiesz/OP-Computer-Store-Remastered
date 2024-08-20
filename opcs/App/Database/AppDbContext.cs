using Microsoft.EntityFrameworkCore;
using opcs.App.Entity.User;
using opcs.App.Model;
using opcs.App.Model.Supplier;
using opcs.App.Model.User;

namespace opcs.App.Database;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        Database.GetDbConnection().Open();
    }

    public DbSet<Supplier> Supplier { get; set; }

    public DbSet<SupplyOrder> SupplyOrder { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<User> User { get; set; }
}