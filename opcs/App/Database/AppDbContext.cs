using Microsoft.EntityFrameworkCore;
using opcs.App.Data.Dto.Pagination;
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

    public DbSet<Supplier> Supplier { get; set; }

    public DbSet<SupplyOrder> SupplyOrder { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<User> User { get; set; }

    public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }

    public DbSet<AccessPermission> AccessPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthRefreshToken>()
            .HasIndex(authRefreshToken => authRefreshToken.UserId)
            .IsUnique();

        modelBuilder.Entity<AuthRefreshToken>()
            .HasOne(authRefreshToken => authRefreshToken.User)
            .WithOne()
            .HasForeignKey<AuthRefreshToken>(authRefreshToken => authRefreshToken.UserId)
            .HasPrincipalKey<User>(user => user.UserId);

        modelBuilder.Entity<AccessPermission>()
            .HasIndex(accessPermission => new {accessPermission.Permission, accessPermission.RoleId})
            .IsUnique();

        modelBuilder.Entity<AccessPermission>()
            .HasOne(accessPermission => accessPermission.Role)
            .WithOne()
            .HasForeignKey<AccessPermission>(accessPermission => accessPermission.RoleId)
            .HasPrincipalKey<Role>(role => role.RoleId);
    }
}