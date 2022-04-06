using System;
using System.Collections.Immutable;
using FsCheck;

namespace ReportRepair2;

public class InputGen
{

    public const int MinTriplets = 2;
    public const int MaxTriplets = 20;

    private static readonly ImmutableList<int> empty = ImmutableList<int>.Empty;


    private static Gen<ImmutableList<T>> GenReduce<T>(ImmutableList<T> items, int count, Func<ImmutableList<T>, Gen<ImmutableList<T>>> acc)
    {
        Gen<ImmutableList<T>> GenUntilDone(ImmutableList<T> items, int remaining) =>
            remaining == 0
                ? Gen.Constant(items)
                : acc(items).SelectMany(it => GenUntilDone(it, remaining - 1));

        return GenUntilDone(items, count);
    }

    private static Gen<ImmutableList<int>> AddExpense(ImmutableList<int> taken)
    {
        bool AddsUpTo2020(int expense)
        {
            for (var i = 0; i < taken.Count - 1; i++)
                for (var o = i + 1; o < taken.Count; o++)
                    if (expense + taken[i] + taken[o] == 2020)
                        return true;
            return false;
        }

        bool CanAdd(int expense) =>
            !taken.Contains(expense) && !AddsUpTo2020(expense);

        return Gen.Choose(Input.MinExpense, Input.MaxExpense)
                  .Where(CanAdd)
                  .Select(taken.Add);
    }

    private static Gen<ImmutableList<int>> Gen2020Triplet()
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
                    third => empty.Add(first)
                                  .Add(second)
                                  .Add(third))));
    }

    private static Gen<ImmutableList<int>> AddTriplet(ImmutableList<int> taken) =>
        GenReduce(taken, 3, AddExpense);

    private static Gen<Input> GenInputOfSize(int tripletCount) =>
        Gen2020Triplet()
            .SelectMany(it => GenReduce(it, tripletCount - 1, AddTriplet))
            .Select(it => it.Shuffle())
            .Select(it => new Input(it.ToImmutableList()));

    private static Gen<Input> GenInput() =>
        Gen.Sized(s => GenInputOfSize(Math.Clamp(s, MinTriplets, MaxTriplets)));

    public static Arbitrary<Input> ArbInput() =>
        Arb.From(GenInput());

}