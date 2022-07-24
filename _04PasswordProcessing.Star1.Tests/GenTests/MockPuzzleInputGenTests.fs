namespace AdventOfCode20.PasswordProcessing.Star1

open AdventOfCode20.PasswordProcessing
open AdventOfCode20.Props
open AdventOfCode20.PasswordProcessing.Star1.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPuzzleInput> |])>]
module MockPuzzleInputGenTests =

    let private passportCount input =
        let (Passports passports) = input.Batch
        passports |> List.length

    [<Property>]
    let ``Generator has variety`` () = genInput |> generatesVariety 10 10

    [<Property>]
    let ``Generator uses size for passport-count`` () =
        genInput |> usesSizeFor passportCount
