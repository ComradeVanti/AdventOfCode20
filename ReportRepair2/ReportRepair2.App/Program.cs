using ReportRepair2;

var input = File.ReadAllLines(args[0])
                .Select(int.Parse);

var output = Report.CalcVerificationNum(input);

Console.WriteLine($"Output is: {output}");