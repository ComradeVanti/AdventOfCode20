using AdapterArray2;

var input = File.ReadAllLines(args[0])
                .Select(int.Parse);

var output = JoltageAdapter.CountConfigurations(input);

Console.WriteLine($"Output is {output}");