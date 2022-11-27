namespace peikcad.mms.domain.shared.patterns.abstractions;

public interface IRepository<out TContext>
{
    IUnitOfWork UnitOfWork { get; }

    TContext CreateNewContext();
}