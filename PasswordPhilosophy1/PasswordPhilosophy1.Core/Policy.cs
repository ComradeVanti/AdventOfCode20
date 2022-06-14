namespace PasswordPhilosophy1;

public record Policy(char Character, int MinCount, int MaxCount)
{

    public const int MinMinCount = 1;
    public const int MaxMinCount = 15;
    public const int MinRange = 1;
    public const int MaxRange = 5;

}