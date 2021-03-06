[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.TobogganTrajectory.Parse

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory

let private parseTile char =
    if char = '.' then Some Empty
    elif char = '#' then Some Tree
    else None

let private parseRow line =
    line |> Seq.map parseTile |> Seq.toList |> Option.collect

let forest (lines: string list) =
    lines
    |> List.map parseRow
    |> Option.collect
    |> Option.map Forest.fromTiles
