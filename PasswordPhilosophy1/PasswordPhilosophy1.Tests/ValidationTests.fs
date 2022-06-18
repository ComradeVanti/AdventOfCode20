namespace PasswordPhilosophy1

open FsCheck.Xunit
open PasswordPhilosophy1.PasswordGen
open PasswordPhilosophy1.PuzzleInputGen

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs>; typeof<ArbPuzzleInput> |])>]
module ValidationTests =

    [<Property>]
    let ``Valid logs are correctly categorized`` (ValidLog log) =
        log |> Validation.IsValid

    [<Property>]
    let ``Invalid logs are correctly categorized`` (InvalidLog log) =
        log |> (not << Validation.IsValid)

    [<Property>]
    let ``Correct number of valid logs is found`` (input: PuzzleInput) =
        input.Entries |> Validation.CountValid = input.ValidCount
