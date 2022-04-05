using FsCheck.Xunit;
using ReportRepair1;

[assembly: Properties(Arbitrary = new[] { typeof(InputGen) })]