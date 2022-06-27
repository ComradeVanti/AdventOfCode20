[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.ReportRepair.Day1.Lib

open AdventOfCode20
open AdventOfCode20.ReportRepair

let private find2020Pair (Entries expenses) =
    expenses
    |> List.pairs
    |> Seq.tryFind (fun (a, b) -> a + b = 2020)


let private mult (a, b) = a * b

let mult2020Pair report = report |> find2020Pair |> Option.map mult
