namespace peikcad.mms.domain.shared.patterns.abstractions;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}