using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using peikcad.mms.domain.shared.patterns.abstractions;

namespace peikcad.mms.application.shared.patterns.commandBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandBus(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(ServiceProviderCommandBus))!;

        assembly.GetTypes()
            .Where(t => typeof(IAsyncCommand<>).IsAssignableFrom(t))
            .Iter(t => services.AddScoped(typeof(IAsyncCommand<>), t));

        assembly.GetTypes()
            .Where(t => typeof(IRepository<>).IsAssignableFrom(t))
            .Iter(t => services.AddScoped(t.GetInterfaces().First(i => typeof(IRepository<>).IsAssignableFrom(i)), t));

        return services
            .AddScoped<ICommandBus, ServiceProviderCommandBus>();
    }
}