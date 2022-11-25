namespace peikcad.mms.application.shared.patterns.commandBus;

public interface ICommandBus
{
    TResult Execute<TCommand, TResult>(in TCommand command)
        where TCommand : ICommand<TResult>;

    Task<TResult> ExecuteAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        where TCommand : IAsyncCommand<Task<TResult>>;
}