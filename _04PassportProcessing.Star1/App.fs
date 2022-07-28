module AdventOfCode20.PassportProcessing.Star1.App

open System
open AdventOfCode20
open AdventOfCode20.PassportProcessing
open AdventOfCode20.PassportProcessing.Star1

[<EntryPoint>]
let main args =
    let path = args |> Array.tryItem 0
    let lines = path |> Option.bind IO.tryReadLines
    let batch = lines |> Option.bind Parse.batch

    match batch |> Option.map countValid with
    | Some validCount ->
        printfn $"Valid: %d{validCount}"
        0
    | None ->
        printfn "Could not calculate product"
        1
