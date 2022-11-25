namespace peikcad.mms.application.shared.patterns.commandBus;

public interface IAsyncCommandHandler<in TCommand, TResult>
{
    Task<TResult> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}