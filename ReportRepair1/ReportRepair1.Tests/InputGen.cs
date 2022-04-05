using FsCheck;

namespace ReportRepair1;

public static class InputGen
{

    private static Gen<int> GenExpense(int sum, Input input)
    {
        bool IsValid(int expense)
        {
            var compliment = sum - expense;

            return input.CanAdd(expense) && input.CanAdd(compliment);
        }

        return Gen.Choose(0, sum)
                  .Where(IsValid);
    }

    private static Gen<(int, int)> GenExpensePairFor(int sum, Input input) =>
        GenExpense(sum, input)
            .Select(it => (it, sum - it));

    private static Gen<Input> GenLargerFor(int sum, Input input) =>
        GenExpensePairFor(sum, input)
            .Select(input.AddPair);

    private static Gen<Input> GenInitial() =>
        GenLargerFor(2020, Input.empty);

    private static Gen<Input> GenLargerReport(Input input) =>
        Gen.Choose(100, 2019)
           .SelectMany(sum => GenLargerFor(sum, input));

    private static Gen<Input> GenReport()
    {
        Gen<Input> GenUntilDone(Input input, int remaining) =>
            remaining == 0
                ? Gen.Constant(input)
                : GenLargerReport(input)
                    .SelectMany(it => GenUntilDone(it, remaining - 1));

        return Gen.Sized(
            s => GenInitial()
                 .SelectMany(initial => GenUntilDone(initial, s))
                 .Select(it => it.ShuffleExpenses()));
    }

    public static Arbitrary<Input> ArbReport() =>
        Arb.From(GenReport());

}