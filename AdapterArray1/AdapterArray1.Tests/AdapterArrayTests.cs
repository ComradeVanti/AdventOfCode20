using System.Linq;
using FsCheck.Xunit;

namespace AdapterArray1;

public class AdapterArrayTests
{

    [Property]
    public bool OutputIsPositive(Input input) =>
        AdapterArray.CalcJoltageRating(input.Joltages) > 0;
    
}