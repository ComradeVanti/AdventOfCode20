using FsCheck.Xunit;

namespace ReportRepair2;

public class ReportTests
{

    private const int MinNum = 4893450;
    private const int MaxNum = 305258030;


    [Property]
    public bool NumberIsInCorrectRange(Input input)
    {
        var num = Report.CalcVerificationNum(input.Expenses);

        return num is >= MinNum and <= MaxNum;
    }

    [Property]
    public bool CorrectResultIsCalculated(Input input) =>
        Report.CalcVerificationNum(input.Expenses) == input.CorrectResult;

}