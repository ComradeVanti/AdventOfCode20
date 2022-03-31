using System.Linq;
using FsCheck.Xunit;

namespace AdapterArray1;

public class AdapterArrayTests
{

    [Property]
    public bool OutputIsPositive(Input input) =>
        AdapterArray.CalcJoltageRating(input.Joltages) > 0;

    [Property]
    public bool DiffBetweenInputsAndInputsPlusOneIsOne(Input original)
    {
        var plusOne = new Input(original.Joltages.Select(it => it + 1).ToArray());

        var originalOutput = AdapterArray.CalcJoltageRating(original.Joltages);
        var plusOneOutput = AdapterArray.CalcJoltageRating(plusOne.Joltages);

        return originalOutput + 1 == plusOneOutput;
    }
    
}