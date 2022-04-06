namespace ReportRepair2;

public static class EnumerableExt
{

    public static IEnumerable<IEnumerable<T>> Triplets<T>(this IEnumerable<T> items)
    {
        var array = items.ToArray();

        IEnumerable<T> TripletsAt(int i, int o, int p)
        {
            yield return array[i];
            yield return array[o];
            yield return array[p];
        }

        for (var i = 0; i < array.Length - 2; i++)
            for (var o = i + 1; o < array.Length - 1; o++)
                for (var p = o + 1; p < array.Length; p++)
                    yield return TripletsAt(i, o, p);
    }

    public static int Product(this IEnumerable<int> nums) =>
        nums.Aggregate(1, (n1, n2) => n1 * n2);

}