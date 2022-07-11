[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.Star1.Lib

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory

let countCollisions map =

    let rows = ForestMap.rowsOf map
    let rowCount = rows |> List.length

    let rec countCollision' position index =
        if index = rowCount then
            0
        else
            let hasTree = map |> ForestMap.hasTreeAt (position, index)
            let count = if hasTree then 1 else 0
            let restCount = countCollision' (position + 3) (index + 1)
            count + restCount

    countCollision' 3 1
