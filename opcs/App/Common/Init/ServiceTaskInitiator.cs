namespace opcs.App.Common.Init;

public class ServiceTaskInitiator(IServiceProvider serviceProvider, IServiceCollection serviceCollection)
    : IHostedService, ITaskInitiator
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateAsyncScope())
        {
            foreach (var service in GetServices()) scope.ServiceProvider.GetService(service);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private IEnumerable<Type> GetServices()
    {
        return serviceCollection
            .Where(service => service.ImplementationType != typeof(ITaskInitiator))
            .Where(descriptor => descriptor.ServiceType.ContainsGenericParameters == false)
            .Select(service => service.ServiceType)
            .Distinct();
    }
}