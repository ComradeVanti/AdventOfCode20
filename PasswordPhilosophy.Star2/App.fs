open System
open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy
open AdventOfCode20.PasswordPhilosophy.Star2

[<EntryPoint>]
let main args =
    let path = args |> Array.tryItem 0
    let lines = path |> Option.bind IO.tryReadLines
    let report = lines |> Option.bind Parse.passwordReport

    match report |> Option.map countValid with
    | Some count ->
        printfn $"Valid entries: %d{count}"
        0
    | None ->
        printfn "Could not count valid entries"
        1
