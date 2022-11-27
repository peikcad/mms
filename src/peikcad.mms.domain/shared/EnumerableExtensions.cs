namespace peikcad.mms.domain.shared;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Iter<T>(this IEnumerable<T> enumerable, Action<T> handle)
    {
        foreach (var item in enumerable)
            handle(item);

        return enumerable;
    }
}