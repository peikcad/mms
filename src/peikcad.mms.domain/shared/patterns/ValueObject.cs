namespace peikcad.mms.domain.shared.patterns
{
    public abstract class ValueObject
    {
        private readonly object[] id;

        protected ValueObject(params object[] id)
        {
            this.id = id;
        }

        public bool IsSameAs(ValueObject compared)
            => ReferenceEquals(compared, this) ||
                compared.id.SequenceEqual(id);

        public override string ToString() => $"{GetType().Name}::{string.Join("//", id)}";
    }
}