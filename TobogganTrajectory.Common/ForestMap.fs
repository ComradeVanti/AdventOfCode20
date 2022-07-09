[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.TobogganTrajectory.ForestMap

open AdventOfCode20

let rowsOf (Tiles tiles) = tiles

let widthOf map =
    rowsOf map
    |> List.tryHead
    |> Option.map List.length
    |> Option.defaultValue 0

let heightOf map = rowsOf map |> List.length

let mapTileAt (x, y) f map =
    rowsOf map
    |> List.mapAt y (List.mapAt x f)
    |> Tiles

let setTileAt (x, y) tile map = map |> mapTileAt (x, y) (fun _ -> tile)
