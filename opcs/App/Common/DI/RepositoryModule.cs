using Autofac;
using opcs.App.Repository.Security;
using opcs.App.Repository.Security.Interface;
using opcs.App.Repository.Supplier;
using opcs.App.Repository.Supplier.Interface;
using opcs.App.Repository.User;
using opcs.App.Repository.User.Interface;

namespace opcs.App.Common.DI;

public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SupplierRepository>()
            .As<ISupplierRepository>()
            .PropertiesAutowired();

        builder.RegisterType<RoleRepository>()
            .As<IRoleRepository>()
            .PropertiesAutowired();

        builder.RegisterType<UserRepository>()
            .As<IUserRepository>()
            .PropertiesAutowired();

        builder.RegisterType<AuthRefreshTokenRepository>()
            .As<IAuthRefreshTokenRepository>()
            .PropertiesAutowired();

        builder.RegisterType<AccessPermissionRepository>()
            .As<IAccessPermissionRepository>()
            .PropertiesAutowired();
    }
}