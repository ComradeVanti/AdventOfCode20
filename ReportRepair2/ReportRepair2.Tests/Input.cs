using System.Collections.Immutable;
using System.Linq;

namespace ReportRepair2;

public readonly struct Input
{

    public const int MinExpense = 50;
    public const int MaxExpense = 3000;


    public ImmutableList<int> Expenses { get; }


    public Input(ImmutableList<int> expenses) =>
        Expenses = expenses;


    public override string ToString()
    {
        var list = Expenses.Chunk(10)
                           .Select(chunk => chunk.Stitch(", "))
                           .Stitch("\n");
        return $"[{list}]";
    }

}