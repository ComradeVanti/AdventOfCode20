using System.Linq;
using FsCheck.Xunit;

namespace AdapterArray1;

public class AdapterArrayTests
{

    [Property]
    public bool OutputIsZeroOrLarger(Input input) =>
        AdapterArray.CalcJoltageRating(input.Joltages) >= 0;

    [Property]
    public bool OutputIsSmallerThanOrEqualToInputCount(Input input) =>
        AdapterArray.CalcJoltageRating(input.Joltages) <= input.Joltages.Length;

}