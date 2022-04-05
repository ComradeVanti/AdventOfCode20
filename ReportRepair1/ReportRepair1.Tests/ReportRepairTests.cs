using FsCheck.Xunit;

namespace ReportRepair1;

public class ReportRepairTests
{

    [Property]
    public bool OutputIsCorrect(Input input)
    {
        var output = ReportRepair.CalcRepairNum(input.Expenses);
        return output == input.CorrectSolution;
    }

}