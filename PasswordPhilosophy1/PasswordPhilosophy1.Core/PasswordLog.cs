namespace PasswordPhilosophy1;

public record PasswordLog(string Password, Policy Policy)
{

    public override string ToString() =>
        $"{Policy.MinCount}-{Policy.MaxCount} {Policy.Character}: {Password}";

}