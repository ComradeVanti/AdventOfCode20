using System;
using FsCheck;

namespace ReportRepair2;

public class InputGen
{

    public const int MinTriplets = 2;
    public const int MaxTriplets = 20;

    private static Gen<Input> GenReduce(Input input, int count, Func<Input, Gen<Input>> acc)
    {
        Gen<Input> GenUntilDone(Input input, int remaining) =>
            remaining == 0
                ? Gen.Constant(input)
                : acc(input).SelectMany(it => GenUntilDone(it, remaining - 1));

        return GenUntilDone(input, count);
    }

    private static Gen<Input> AddExpense(Input input)
    {
        bool AddsUpTo2020(int expense)
        {
            for (var i = 0; i < input.Count - 1; i++)
                for (var o = i + 1; o < input.Count; o++)
                    if (expense + input[i] + input[o] == 2020)
                        return true;
            return false;
        }

        bool CanAdd(int expense) =>
            !input.Contains(expense) && !AddsUpTo2020(expense);

        return Gen.Choose(Input.MinExpense, Input.MaxExpense)
                  .Where(CanAdd)
                  .Select(input.Add);
    }

    private static Gen<Input> Gen2020Triplet()
    {
        Gen<int> GenFirst() =>
            Gen.Choose(Input.MinExpense, 670);

        Gen<int> GenSecond(int first) =>
            Gen.Choose(Input.MinExpense, 670)
               .Where(it => it != first);

        Gen<int> GenThird(int first, int second) =>
            Gen.Constant(2020 - (first + second));

        return GenFirst().SelectMany(
            first => GenSecond(first).SelectMany(
                second => GenThird(first, second).Select(
                    third => new Input(first, second, third))));
    }

    private static Gen<Input> AddTriplet(Input input) =>
        GenReduce(input, 3, AddExpense);

    private static Gen<Input> GenInputOfSize(int tripletCount) =>
        Gen2020Triplet()
            .SelectMany(it => GenReduce(it, tripletCount - 1, AddTriplet))
            .Select(it => it.Shuffle());

    private static Gen<Input> GenInput() =>
        Gen.Sized(s => GenInputOfSize(Math.Clamp(s, MinTriplets, MaxTriplets)));

    public static Arbitrary<Input> ArbInput() =>
        Arb.From(GenInput());

}