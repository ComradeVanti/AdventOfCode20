using PasswordPhilosophy1;

var path = args[0];
var lines = File.ReadLines(path);
var logs = lines.Select(PasswordLog.Parse);
var validCount = Validation.CountValid(logs!);

Console.WriteLine($"Valid: {validCount}");