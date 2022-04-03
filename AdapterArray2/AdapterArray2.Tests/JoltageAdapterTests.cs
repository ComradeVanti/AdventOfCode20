using System.Linq;
using FsCheck.Xunit;
using Xunit;
using static System.Array;
using static AdapterArray2.JoltageAdapter;

namespace AdapterArray2;

public class JoltageAdapterTests
{

    [Fact]
    public void EmptyInputResultsInOne() =>
        Assert.Equal(1, CountConfigurations(Empty<int>()));

    [Property]
    public bool ThereIsAtLeastOneConfiguration(Input input) =>
        CountConfigurations(input) >= 1;

    [Property]
    public bool InputOrderHasNoEffect(Input input) =>
        CountConfigurations(input) ==
        CountConfigurations(input.Shuffle().ToArray());

}