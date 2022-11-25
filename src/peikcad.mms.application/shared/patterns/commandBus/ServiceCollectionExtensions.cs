using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace peikcad.mms.application.shared.patterns.commandBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommandBus(this IServiceCollection services)
    {
        services.AddScoped<ICommandBus, ServiceProviderCommandBus>();

        var assembly = Assembly.GetAssembly(typeof(ServiceProviderCommandBus))!;
        
        var syncCommandTypes = assembly.GetTypes()
            .Where(t => typeof(ICommand<>).IsAssignableFrom(t));

        foreach (var type in syncCommandTypes)
            services.AddScoped(typeof(ICommand<>), type);
        
        var asyncCommandTypes = assembly.GetTypes()
            .Where(t => typeof(IAsyncCommand<>).IsAssignableFrom(t));
        
        foreach (var type in asyncCommandTypes)
            services.AddScoped(typeof(IAsyncCommand<>), type);

        return services;
    }
}