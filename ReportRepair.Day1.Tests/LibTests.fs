namespace AdventOfCode20.ReportRepair.Day1

open AdventOfCode20.Props
open AdventOfCode20.ReportRepair
open AdventOfCode20.ReportRepair.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbMockPuzzleInput> |])>]
module LibTests =

    [<Property>]
    let ``Finds correct product`` input =
        match mult2020Pair input.Report with
        | Some product -> product =? input.PairProduct
        | None -> rejectWith "No 2020 pair found."
