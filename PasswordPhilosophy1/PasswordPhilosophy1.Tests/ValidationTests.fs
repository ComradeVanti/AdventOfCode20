namespace PasswordPhilosophy1

open FsCheck.Xunit
open PasswordPhilosophy1.PasswordGen

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module ValidationGenTests =

    [<Property>]
    let ``Valid logs are correctly categorized`` (ValidLog log) =
        log |> Validation.IsValid

    [<Property>]
    let ``Invalid logs are correctly categorized`` (InvalidLog log) =
        log |> (not << Validation.IsValid)
