using System.Text.RegularExpressions;

namespace PasswordPhilosophy1;

public static class RegexExt
{

    public static Match? TryMatch(this Regex r, string s)
    {
        var match = r.Match(s);
        return match.Success ? match : null;
    }

}