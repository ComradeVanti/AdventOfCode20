namespace AdapterArray2;

public static class EnumerableExt
{

    public static IEnumerable<T> Sort<T>(this IEnumerable<T> items) =>
        items.OrderBy(it => it);

    public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> items)
    {
        var array = items.ToArray();

        for (var i = 0; i < array.Length - 1; i++)
            yield return (array[i], array[i + 1]);
    }

}