namespace PasswordPhilosophy1;

public static class StringExt
{

    public static int CountChar(this string s, char c) =>
        s.Count(it => it == c);

}