using System.Diagnostics.CodeAnalysis;

namespace peikcad.mms.domain
{
    public record Result<T>
    {
        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success { get; }
        
        public T? Value { get; }
        
        public Exception? Error { get; }

        public T OrThrow() => Success ? Value : throw Error;

        public Result<TMapped> Map<TMapped>(Func<T, TMapped> map) => Success ? new Result<TMapped>(map(Value)) : new(Error);

        internal Result(T value)
        {
            Success = true;
            Value = value;
            Error = null;
        }

        internal Result(Exception error)
        {
            Success = false;
            Error = error;
            Value = default;
        }
    }
}