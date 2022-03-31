using System.Collections.Generic;
using System.Linq;
using FsCheck;

namespace AdapterArray1;

public static class InputGen
{

    public static Arbitrary<Input> ArbInput() => Arb.From(GenInput());


    private static Gen<int> GenNextJoltage(int lastJoltage) =>
        Gen.Choose(1, 3).Select(it => lastJoltage + it);

    private static Gen<Input> GenInput()
    {
        Gen<IEnumerable<int>> GenUntilDone(int last, int remaining) =>
            remaining == 0
                ? Gen.Constant(Enumerable.Empty<int>())
                : GenNextJoltage(last)
                    .SelectMany(newJoltage => GenUntilDone(newJoltage, remaining - 1)
                                    .Select(it => it.Prepend(newJoltage)));

        return Gen.Sized(joltageCount => GenUntilDone(0, joltageCount))
                  .Select(it => it.Shuffle())
                  .Select(it => new Input(it.ToArray()));
    }

}