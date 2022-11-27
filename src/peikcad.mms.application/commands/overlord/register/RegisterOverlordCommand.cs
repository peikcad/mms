using peikcad.mms.domain.shared.personas;

namespace peikcad.mms.application.commands.overlord.register;

public sealed class RegisterOverlordCommand : IAsyncCommand<IID>
{
    public string IID { get; init; } = string.Empty;
    
    public string Name { get; init; } = string.Empty;
    
    public DateTime BirthDate { get; init; }
}