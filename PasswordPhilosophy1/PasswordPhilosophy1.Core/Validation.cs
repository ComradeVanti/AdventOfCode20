﻿namespace PasswordPhilosophy1;

public static class Validation
{

    public static bool IsValid(PasswordLog log)
    {
        var letterCount = log.Password.CountChar(log.Policy.Character);
        return letterCount >= log.Policy.MinCount && letterCount <= log.Policy.MaxCount;
    }

}