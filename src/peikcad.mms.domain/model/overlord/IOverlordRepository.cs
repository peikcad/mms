using peikcad.mms.domain.shared.patterns.abstractions;
using peikcad.mms.domain.shared.personas;

namespace peikcad.mms.domain.model.overlord;

public interface IOverlordRepository : IRepository<IOverlordContext>
{
    Task<bool> ExistsByIIDAsync(IID id, CancellationToken cancellationToken);

    Task<Overlord> AddAsync(Overlord overlord, CancellationToken cancellationToken);
}