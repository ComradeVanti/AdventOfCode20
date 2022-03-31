namespace AdapterArray1;

public static class IEnumerableExt
{

    public static IEnumerable<T> Sort<T>(this IEnumerable<T> items) =>
        items.OrderBy(it => it);

    public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> items)
    {
        var itemsArray = items.ToArray();
        for (var i = 0; i < itemsArray.Length - 1; i++)
            yield return (itemsArray[i], itemsArray[i + 1]);
    }

    public static int CountItem<T>(this IEnumerable<T> items, T item) =>
        items.Count(it => Equals(it, item));

    public static IEnumerable<int> Diffs(this IEnumerable<int> nums) =>
        nums.Pairwise()
            .Select(tuple =>
            {
                var (fst, snd) = tuple;
                return snd - fst;
            });

}