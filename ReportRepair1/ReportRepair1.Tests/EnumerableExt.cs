using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportRepair1;

public static class EnumerableExt
{

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
    {
        var itemsList = items.ToList();

        var random = new Random();
        while (itemsList.Count > 0)
        {
            var i = random.Next(0, itemsList.Count);
            yield return itemsList[i];
            itemsList.RemoveAt(i);
        }
    }

}