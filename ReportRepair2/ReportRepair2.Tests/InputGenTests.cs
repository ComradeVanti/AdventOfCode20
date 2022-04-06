using System.Linq;
using FsCheck.Xunit;

namespace ReportRepair2;

public class InputGenTests
{

    [Property]
    public bool InputHasCorrectNumberOfItems(Input input) =>
        input.Expenses.Count
            is >= InputGen.MinTriplets * 3
            and <= InputGen.MaxTriplets * 3;

    [Property]
    public bool InputCountIsDivisibleByThree(Input input) =>
        input.Expenses.Count % 3 == 0;

    [Property]
    public bool InputHasNoDuplicates(Input input) =>
        input.Expenses.HasNoDuplicates();

    [Property]
    public bool ExpensesAreInCorrectRange(Input input) =>
        input.Expenses.All(it => it is >= Input.MinExpense and <= Input.MaxExpense);

    [Property(MaxFail = 1)]
    public bool SameInputIsAlmostNeverGeneratedTwice(Input i1, Input i2) =>
        !i1.Expenses.SequenceEqual(i2.Expenses);

    [Property(MaxFail = 1)]
    public bool InputIsAlmostNeverSorted(Input input) =>
        !input.Expenses.IsSorted();

    [Property]
    public bool ExactlyOneTripletAddsUpTo2020(Input input) =>
        input.Expenses.Triplets()
             .Select(it => it.Sum())
             .CountItem(2020) == 1;

}