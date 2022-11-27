namespace peikcad.mms.application.shared.patterns.commandBus;

public interface ICommandBus
{
    Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : IAsyncCommand<Task<TResult>>;
}