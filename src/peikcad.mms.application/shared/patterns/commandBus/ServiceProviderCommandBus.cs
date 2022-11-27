using Microsoft.Extensions.DependencyInjection;

namespace peikcad.mms.application.shared.patterns.commandBus;

public sealed class ServiceProviderCommandBus : ICommandBus
{
    private readonly IServiceProvider serviceProvider;

    public ServiceProviderCommandBus(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : IAsyncCommand<Task<TResult>>
    {
        var handler = serviceProvider.GetService<IAsyncCommandHandler<TCommand, TResult>>();

        if (handler is null)
            throw new($"Unable to find a handler for command type [{command.GetType().Name}]");

        return await handler.ExecuteAsync(command, cancellationToken);
    }
}