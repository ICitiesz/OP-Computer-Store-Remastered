using Microsoft.EntityFrameworkCore;
using opcs.App.Entity.Security;
using opcs.App.Entity.Supplier;
using opcs.App.Entity.Supply;
using opcs.App.Entity.User;

namespace opcs.App.Database;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        Database.GetDbConnection().Open();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthRefreshToken>()
            .HasOne(entity => entity.User)
            .WithMany()
            .HasForeignKey(entity => entity.UserId)
            .HasPrincipalKey(entity => entity.UserId);
    }

    public DbSet<Supplier> Supplier { get; set; }

    public DbSet<SupplyOrder> SupplyOrder { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<User> User { get; set; }

    public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }

    public DbSet<AccessPermission> AccessPermissions { get; set; }
}