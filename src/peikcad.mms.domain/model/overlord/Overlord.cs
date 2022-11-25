using peikcad.mms.domain.shared.personas;

namespace peikcad.mms.domain.model.overlord
{
    public sealed class Overlord : DomainEntity<IID, IOverlordContext>
    {
        public CompleteName Name => CompleteName.Deserialize(DomainContext.Name).OrThrow();

        public DateTime BirthDate => DomainContext.BirthDate;

        public static Result<Overlord> Register(IID id, CompleteName name, DateTime birthDate, Func<IOverlordContext> newContext) => new(
            new Overlord(id, newContext()));

        public static Result<Overlord> Deserialize(IOverlordContext context) => IID.Deserialize(context.IID).Map(id => new Overlord(id, context));

        private Overlord(IID id, IOverlordContext context) : base(id, context)
        {
        }
    }
}