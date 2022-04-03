using AdapterArray2;
using FsCheck.Xunit;

[assembly: Properties(Arbitrary = new[] { typeof(InputGen) })]