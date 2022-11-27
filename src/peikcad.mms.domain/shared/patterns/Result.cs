using System.Diagnostics.CodeAnalysis;

namespace peikcad.mms.domain.shared.patterns
{
    public record Result<T>
    {
        public static ResultBuilder<T> Return => new();
        
        public static AsyncResultBuilder<T> ReturnAsync => new();
        
        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success { get; }
        
        public T? Value { get; }
        
        public Exception? Error { get; }

        public T OrThrow() => Success ? Value : throw Error;

        public Result<TMapped> Map<TMapped>(Func<T, TMapped> map) => Success ? new Result<TMapped>(map(Value)) : new(Error);
        
        public async Task<Result<TMapped>> MapAsync<TMapped>(Func<T, Task<TMapped>> mapAsync)
            => Success ? new Result<TMapped>(await mapAsync(Value)) : new(Error);

        public Result(T value)
        {
            Success = true;
            Value = value;
            Error = null;
        }

        public Result(Exception error)
        {
            Success = false;
            Error = error;
            Value = default;
        }
    }
}