namespace peikcad.mms.domain.shared.patterns;

public sealed class ResultBuilder<T>
{
    private Func<Task<bool>>? preconditionAsync;
    private Func<Task<T>>? tryDoAsync;
    private Func<Exception>? fail;
    
    public ResultBuilder<T> IfAsync(Func<Task<bool>> preconditionAsync)
    {
        this.preconditionAsync = preconditionAsync;
        return this;
    }
    
    public ResultBuilder<T> TryAsync(Func<Task<T>> tryDoAsync)
    {
        this.tryDoAsync = tryDoAsync;
        return this;
    }

    public ResultBuilder<T> Or(Func<Exception> fail)
    {
        this.fail = fail;
        return this;
    }

    public async Task<Result<T>> AsAsync()
    {
        if (preconditionAsync is null)
            throw new ArgumentNullException(nameof(preconditionAsync));

        if (tryDoAsync is null)
            throw new ArgumentNullException(nameof(tryDoAsync));

        if (fail is null)
            throw new ArgumentNullException(nameof(fail));

        if (!await preconditionAsync())
            return new(fail());

        try
        {
            return new(await tryDoAsync());
        }
        catch (Exception e)
        {
            return new(e);
        }
    }
}