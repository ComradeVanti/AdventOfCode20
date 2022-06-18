using System.Text.RegularExpressions;

namespace PasswordPhilosophy1;

public record PasswordLog(string Password, Policy Policy)
{

    private static readonly Regex entryRegex = new Regex(@"(?<minCount>\d+)-(?<maxCount>\d+) (?<letter>[a-z]): (?<password>[a-z]+)");

    public static PasswordLog? Parse(string s)
    {
        var match = entryRegex.TryMatch(s);

        if (match != null)
        {
            var password = match.Groups["password"].Value;
            var letter = match.Groups["letter"].Value[0];
            var minCount = int.Parse(match.Groups["minCount"].Value);
            var maxCount = int.Parse(match.Groups["maxCount"].Value);
            return new PasswordLog(password, new Policy(letter, minCount, maxCount));
        }

        return null;
    }

    public override string ToString() =>
        $"{Policy.MinCount}-{Policy.MaxCount} {Policy.Character}: {Password}";

}