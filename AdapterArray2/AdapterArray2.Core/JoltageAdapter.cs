namespace AdapterArray2;

public static class JoltageAdapter
{

    public static long CountConfigurations(IEnumerable<int> joltages)
    {
        var sorted = joltages.Sort().ToArray();
        var diffs =
            sorted.Prepend(0)
                  .Append(sorted.LastOrDefault(0) + 3)
                  .Diffs()
                  .Append(4)
                  .ToArray();

        var combinations = new long[diffs.Length];
        combinations[^1] = 1;

        for (var i = diffs.Length - 2; i >= 0; i--)
        {
            var jump = 3;
            var o = i;

            while (jump >= diffs[o])
            {
                jump -= diffs[o];
                o++;
                combinations[i] += combinations[o];
            }
        }

        return combinations[0];
    }

    private static IEnumerable<int> Diffs(this IEnumerable<int> joltages) =>
        joltages.Pairwise().Select(it => it.Item2 - it.Item1);

}