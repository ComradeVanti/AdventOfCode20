namespace ReportRepair2;

public static class Report
{

    public static int CalcVerificationNum(IEnumerable<int> expenses) =>
        expenses.Triplets().First(it => it.Sum() == 2020).Product();

}