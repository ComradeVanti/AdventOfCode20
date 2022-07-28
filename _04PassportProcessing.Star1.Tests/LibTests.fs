namespace AdventOfCode20.PassportProcessing.Star1

open AdventOfCode20
open AdventOfCode20.Props
open AdventOfCode20.PassportProcessing
open AdventOfCode20.PassportProcessing.Star1
open AdventOfCode20.PassportProcessing.Star1.MockPuzzleInputGen
open FsCheck.Xunit
open Xunit


[<Properties(Arbitrary = [| typeof<ArbPuzzleInput> |])>]
module LibTests =

    [<Fact>]
    let ``Correct number found in example`` () =
        let batch = ExampleInput.batch |> Option.get
        batch |> countValid =? 2

    [<Property>]
    let ``Correct number of valid entries found`` input =
        input.Batch |> countValid =? input.ValidCount
