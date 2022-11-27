namespace peikcad.mms.domain.shared.patterns;

public sealed class ResultBuilder<T>
{
    private Func<bool>? precondition;
    private Func<T>? tryDo;
    private Func<Exception>? fail;
    
    public ResultBuilder<T> If(Func<bool> precondition)
    {
        this.precondition = precondition;
        return this;
    }
    
    public ResultBuilder<T> Try(Func<T> tryDo)
    {
        this.tryDo = tryDo;
        return this;
    }

    public ResultBuilder<T> Or(Func<Exception> fail)
    {
        this.fail = fail;
        return this;
    }

    public ResultBuilder<T> OrException<TError>(params object[] args)
        where TError : Exception
    {
        fail = () => (TError) Activator.CreateInstance(typeof(TError), args)!;
        return this;
    }

    public Result<T> As()
    {
        if (precondition is null)
            throw new ArgumentNullException(nameof(precondition));

        if (tryDo is null)
            throw new ArgumentNullException(nameof(tryDo));

        if (fail is null)
            throw new ArgumentNullException(nameof(fail));

        if (!precondition())
            return new(fail());

        try
        {
            return new(tryDo());
        }
        catch (Exception e)
        {
            return new(e);
        }
    }
}