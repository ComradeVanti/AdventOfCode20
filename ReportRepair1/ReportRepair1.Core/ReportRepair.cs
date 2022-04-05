using System.Collections.Immutable;

namespace ReportRepair1;

public static class ReportRepair
{

    public static int CalcRepairNum(IEnumerable<int> expenses)
    {
        var expensesArray = expenses.ToImmutableArray();

        for (var i = 0; i < expensesArray.Length - 1; i++)
        {
            var n1 = expensesArray[i];

            for (var o = i + 1; o < expensesArray.Length; o++)
            {
                var n2 = expensesArray[o];
                if (n1 + n2 == 2020)
                    return n1 * n2;
            }
        }

        throw new ArgumentException("No 2020 pair found!");
    }

}