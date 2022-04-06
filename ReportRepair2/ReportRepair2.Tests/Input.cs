using System.Collections.Immutable;
using System.Linq;

namespace ReportRepair2;

public readonly struct Input
{

    public const int MinExpense = 50;
    public const int MaxExpense = 3000;


    public ImmutableList<int> Expenses { get; }

    public ImmutableArray<int> SearchTriplet { get; }

    public int CorrectResult => SearchTriplet.Product();

    public int Count => Expenses.Count;

    public int this[int i] =>
        Expenses[i];


    public Input(int first, int second, int third)
    {
        Expenses = new[] { first, second, third }.ToImmutableList();
        SearchTriplet = Expenses.ToImmutableArray();
    }

    public Input(ImmutableList<int> expenses, ImmutableArray<int> searchTriplet)
    {
        Expenses = expenses;
        SearchTriplet = searchTriplet;
    }


    public bool Contains(int expense) =>
        Expenses.Contains(expense);

    public Input Add(int expense) =>
        new Input(Expenses.Add(expense), SearchTriplet);

    public Input Shuffle() =>
        new Input(Expenses.Shuffle().ToImmutableList(), SearchTriplet);

    public override string ToString()
    {
        var list = Expenses.Chunk(10)
                           .Select(chunk => chunk.Stitch(", "))
                           .Stitch("\n");
        return $"[{list}]";
    }

}