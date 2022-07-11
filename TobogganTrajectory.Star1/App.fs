module AdventOfCode20.TobogganTrajectory.Star1.App

open System
open AdventOfCode20
open AdventOfCode20.TobogganTrajectory
open AdventOfCode20.TobogganTrajectory.Star1

[<EntryPoint>]
let main args =
    let path = args |> Array.tryItem 0
    let lines = path |> Option.bind IO.tryReadLines
    let map = lines |> Option.bind Parse.forestMap

    match map |> Option.map countCollisions with
    | Some collisionCount ->
        printfn $"Collision: %d{collisionCount}"
        0
    | None ->
        printfn "Could not calculate product"
        1
