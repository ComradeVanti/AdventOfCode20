using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdapterArray2;

public readonly struct Input : IEnumerable<int>
{

    public int[] Joltages { get; }


    public Input(int[] joltages) =>
        Joltages = joltages;

    public IEnumerator<int> GetEnumerator() =>
        ((IEnumerable<int>)Joltages).GetEnumerator();

    public override string ToString() =>
        string.Concat(Joltages.Select(it => $"{it}, "));

    IEnumerator IEnumerable.GetEnumerator() =>
        Joltages.GetEnumerator();


    public static implicit operator int[](Input input) =>
        input.Joltages;

}