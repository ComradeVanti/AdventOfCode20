namespace AdventOfCode20.ReportRepair

open AdventOfCode20
open AdventOfCode20.ListProps
open AdventOfCode20.ReportRepair.MockPuzzleInputGen
open FsCheck
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbMockPuzzleInput> |])>]
module MockPuzzleInputGenTests =

    let ``2020PairsIn`` list =
        list
        |> List.pairs
        |> Seq.countWhere (fun (a, b) -> a + b = 2020)

    let ``2020TripletsIn`` list =
        list
        |> List.triplets
        |> Seq.countWhere (fun (a, b, c) -> a + b + c = 2020)


    [<Property>]
    let ``At least 3 items`` input =
        let (Entries expenses) = input.Report
        expenses |> hasAtLeastLengthOf 3

    [<Property>]
    let ``Exactly one pair adds up to 2020`` input =
        let (Entries expenses) = input.Report
        let pairs = ``2020PairsIn`` expenses
        pairs = 1 |@ $"There were %d{pairs} pairs in the list."

    [<Property>]
    let ``Exactly one triplet adds up to 2020`` input =
        let (Entries expenses) = input.Report
        let triplets = ``2020TripletsIn`` expenses

        triplets = 1
        |@ $"There were %d{triplets} triplets in the list."
