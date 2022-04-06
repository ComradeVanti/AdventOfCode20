using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportRepair2;

public static class EnumerableExt
{

    private static IEnumerable<T> Interleave<T>(this IEnumerable<T> items, T insert)
    {
        var array = items.ToArray();

        for (var i = 0; i < array.Length; i++)
        {
            yield return array[i];

            if (i < array.Length - 1)
                yield return insert;
        }
    }

    private static IEnumerable<T> Sort<T>(this IEnumerable<T> items) =>
        items.OrderBy(it => it);

    public static string Stitch(this IEnumerable<string> strings, string separator = "") =>
        string.Concat(strings.Interleave(separator));

    public static string Stitch<T>(this IEnumerable<T> items, string separator = "") =>
        items.Select(it => it?.ToString() ?? "{null}").Stitch(separator);

    public static bool HasNoDuplicates<T>(this IEnumerable<T> items)
    {
        var sorted = items.Sort().ToArray();
        return sorted.SequenceEqual(sorted.Distinct());
    }

    public static bool IsSorted<T>(this IEnumerable<T> items)
    {
        var array = items.ToArray();

        return array.SequenceEqual(array.Sort());
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
    {
        var random = new Random();
        var list = items.ToList();

        while (list.Count > 0)
        {
            var i = random.Next(0, list.Count);
            yield return list[i];
            list.RemoveAt(i);
        }
    }

    public static int CountItem<T>(this IEnumerable<T> items, T item) =>
        items.Count(it => Equals(it, item));

}