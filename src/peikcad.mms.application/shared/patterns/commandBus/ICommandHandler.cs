namespace peikcad.mms.application.shared.patterns.commandBus;

public interface ICommandHandler<TCommand, out TResult>
    where TCommand : ICommand<TResult>
{
    TResult Execute(in TCommand command);
}