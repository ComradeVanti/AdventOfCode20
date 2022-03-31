namespace AdapterArray1;

public readonly struct Input
{

    public int[] Joltages { get; }


    public Input(int[] joltages) =>
        Joltages = joltages;

}