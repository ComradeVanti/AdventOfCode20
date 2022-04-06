using FsCheck.Xunit;
using ReportRepair2;

[assembly: Properties(Arbitrary = new[] { typeof(InputGen) })]