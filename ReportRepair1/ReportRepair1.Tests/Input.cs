using System;
using System.Collections.Immutable;
using System.Linq;

namespace ReportRepair1;

public readonly struct Input
{

    public static readonly Input empty = new Input(ImmutableList<int>.Empty,
                                                   ImmutableList<(int, int)>.Empty);


    public ImmutableList<int> Expenses { get; }

    public ImmutableList<(int, int)> Pairs { get; }

    public (int, int) SearchPair => Pairs.First();

    public int Count => Expenses.Count;

    public int CorrectSolution => SearchPair.Item1 * SearchPair.Item2;


    public Input(ImmutableList<int> expenses, ImmutableList<(int, int)> pairs)
    {
        Expenses = expenses;
        Pairs = pairs;
    }


    public Input AddPair((int, int) pair)
    {
        var random = new Random();
        var count = Count;

        int Index() => random.Next(0, count);

        return new Input(
            Expenses.Insert(Index(), pair.Item1).Insert(Index(), pair.Item2),
            Pairs.Add(pair));
    }

    public bool CanAdd(int expense) =>
        !Expenses.Contains(expense) &&
        Expenses.All(it => it + expense != 2020);

    public Input ShuffleExpenses() =>
        new Input(Expenses.Shuffle().ToImmutableList(),
                  Pairs);

}