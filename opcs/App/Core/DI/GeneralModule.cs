using Autofac;
using opcs.App.Database;

namespace opcs.App.Core.DI;

public class GeneralModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AppDbContext>().InstancePerLifetimeScope();
        builder.RegisterType<AppConfiguration>().InstancePerLifetimeScope();
    }
}