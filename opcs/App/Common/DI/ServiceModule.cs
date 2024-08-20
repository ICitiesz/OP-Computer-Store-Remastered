using Autofac;
using opcs.App.Service.Security;
using opcs.App.Service.Security.Interface;
using opcs.App.Service.Supplier;
using opcs.App.Service.Supplier.Interface;
using opcs.App.Service.User;
using opcs.App.Service.User.Interface;

namespace opcs.App.Common.DI;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SupplierService>().As<ISupplierService>().PropertiesAutowired();
        builder.RegisterType<RoleService>().As<IRoleService>().PropertiesAutowired();
        builder.RegisterType<UserService>().As<IUserService>().PropertiesAutowired();
        builder.RegisterType<SecurityService>().As<ISecurityService>().PropertiesAutowired();
    }
}