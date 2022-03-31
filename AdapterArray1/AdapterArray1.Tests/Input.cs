using System.Linq;

namespace AdapterArray1;

public readonly struct Input
{

    public int[] Joltages { get; }


    public Input(int[] joltages) =>
        Joltages = joltages;

    public override string ToString() =>
        string.Concat(Joltages.Select(it => $"{it}, "));

}