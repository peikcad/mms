using peikcad.mms.domain.model.overlord;
using peikcad.mms.domain.shared.personas;

namespace peikcad.mms.application.commands.overlord.register;

public sealed class RegisterOverlordCommandHandler : IAsyncCommandHandler<RegisterOverlordCommand, IID>
{
    private readonly IOverlordRepository repository;

    public RegisterOverlordCommandHandler(IOverlordRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IID> ExecuteAsync(RegisterOverlordCommand command, CancellationToken cancellationToken)
    {
        var iid = IID.Deserialize(command.IID).OrThrow();
        var name = CompleteName.Deserialize(command.Name).OrThrow();

        return (await (await Result<Overlord>.ReturnAsync
                    .IfAsync(async () => false == await repository.ExistsByIIDAsync(iid, cancellationToken))
                    .TryAsync(() => repository.AddAsync(
                        Overlord.Register(iid, name, command.BirthDate, repository.CreateNewContext).OrThrow(), cancellationToken))
                    .OrException<EntityDuplicationException>()
                    .AsAsync())
                .MapAsync(async o =>
                {
                    await repository.UnitOfWork.CommitAsync(cancellationToken);
                    return o.DomainId;
                }))
            .OrThrow();
    }
}