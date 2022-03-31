using AdapterArray1;
using FsCheck.Xunit;

[assembly: Properties(Arbitrary = new[] { typeof(InputGen) })]