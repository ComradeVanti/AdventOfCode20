﻿using ReportRepair1;

var path = args[0];

var input = File.ReadLines(path).Select(int.Parse).ToArray();

var output = ReportRepair.CalcRepairNum(input);

Console.WriteLine($"The output is: {output}");