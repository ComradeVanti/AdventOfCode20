using System.Collections.Generic;
using System.Linq;
using FsCheck.Xunit;

namespace ReportRepair1;

public class InputGenTests
{

    [Property]
    public bool ExactlyOneOfThePairsAddUpTo2020(Input input)
    {
        IEnumerable<int> Sums()
        {
            var expenses = input.Expenses;
            for (var i = 0; i < expenses.Count - 1; i++)
            {
                for (var o = i + 1; o < expenses.Count; o++)
                    yield return expenses[i] + expenses[o];
            }
        }

        return Sums().Count(it => it == 2020) == 1;
    }

}