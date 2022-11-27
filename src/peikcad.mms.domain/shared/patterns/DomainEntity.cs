namespace peikcad.mms.domain.shared.patterns
{
    public abstract class DomainEntity<TId, TContext>
    {
        public TId DomainId { get; }
        
        public TContext DomainContext { get; }

        protected DomainEntity(TId id, TContext context)
        {
            DomainId = id;
            DomainContext = context;
        }

        public bool IsSameAs(DomainEntity<TId, TContext> compared)
            => ReferenceEquals(compared, this) ||
               EqualityComparer<TId>.Default.Equals(compared.DomainId, DomainId);

        public override string ToString() => $"{GetType().Name}::{DomainId}";
    }
}