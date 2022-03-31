using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace AdapterArray1;

public class AdapterArrayTests
{

    [Property]
    public bool OutputIsZeroOrLarger(Input input) =>
        AdapterArray.CalcJoltageRating(input.Joltages) >= 0;

    [Property]
    public bool InputOrderHasNoImpactOnOutput(Input input)
    {
        var unsorted = AdapterArray.CalcJoltageRating(input.Joltages);
        var sorted = AdapterArray.CalcJoltageRating(input.Joltages.OrderBy(it => it).ToArray());

        return unsorted == sorted;
    }

    [Property]
    public bool InputWithOnlyOneDifferencesResultsInInputCount(PositiveInt count)
    {
        var input = new Input(Enumerable.Range(1, count.Item).ToArray());

        return AdapterArray.CalcJoltageRating(input.Joltages) == input.Joltages.Length;
    }
    
}