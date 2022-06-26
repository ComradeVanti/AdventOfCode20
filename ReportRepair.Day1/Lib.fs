[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.ReportRepair.Day1.Lib

open AdventOfCode20
open AdventOfCode20.ReportRepair

let private find2020Pair (Entries expenses) =

    let tryFindPairFrom index =
        let expense = expenses |> List.item index

        expenses
        |> List.skip (index + 1)
        |> List.tryFind (fun other -> other + expense = 2020)
        |> Option.map (fun other -> (expense, other))

    expenses |> List.indices |> List.tryPick tryFindPairFrom


let private mult (a, b) = a * b

let mult2020Pair report = report |> find2020Pair |> Option.map mult
