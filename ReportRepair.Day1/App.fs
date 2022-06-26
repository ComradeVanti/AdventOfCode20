module AdventOfCode20.ReportRepair.Day1.App

open System
open AdventOfCode20
open AdventOfCode20.ReportRepair
open AdventOfCode20.ReportRepair.Day1

[<EntryPoint>]
let main args =
    let path = args |> Array.tryItem 0
    let lines = path |> Option.bind IO.tryReadLines
    let report = lines |> Option.bind Parse.expenseReport

    match report |> Option.bind mult2020Pair with
    | Some product ->
        printfn $"Product of 2020 pair: %d{product}"
        0
    | None ->
        printfn "Could not calculate product"
        1
