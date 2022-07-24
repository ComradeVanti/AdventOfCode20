[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.ReportRepair.Star2.Lib

open AdventOfCode20
open AdventOfCode20.ReportRepair

let private find2020Triplet (Entries expenses) =
    expenses
    |> List.triplets
    |> Seq.tryFind (fun (a, b, c) -> a + b + c = 2020)


let private mult (a, b, c) = a * b * c

let mult2020Triplet report = report |> find2020Triplet |> Option.map mult
