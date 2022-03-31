using System;
using System.Collections.Generic;
using System.Linq;

namespace AdapterArray1;

public static class IEnumerableExt
{

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
    {
        var itemsList = items.ToList();
        var rand = new Random();

        while (itemsList.Count > 0)
        {
            var index = rand.Next(0, itemsList.Count);
            yield return itemsList[index];
            itemsList.RemoveAt(index);
        }
    }

}