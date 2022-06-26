namespace AdventOfCode20.ReportRepair

open AdventOfCode20
open AdventOfCode20.ListProps
open AdventOfCode20.ReportRepair.MockPuzzleInputGen
open FsCheck
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbMockPuzzleInput> |])>]
module MockPuzzleInputGenTests =

    let ``2020PairsIn`` list =

        let ``2020PairsWith`` (index, expense) =
            List.skip (index + 1) list
            |> List.map ((+) expense)
            |> List.countWith ((=) 2020)

        list
        |> List.indexed
        |> List.map ``2020PairsWith``
        |> List.sum


    [<Property>]
    let ``At least 2 items`` input =
        let (Entries expenses) = input.Report
        expenses |> hasAtLeastLengthOf 2

    [<Property>]
    let ``Exactly one pair adds up to 2020`` input =
        let (Entries expenses) = input.Report
        let pairs = ``2020PairsIn`` expenses
        pairs = 1 |@ $"There were %d{pairs} pairs in the list."
