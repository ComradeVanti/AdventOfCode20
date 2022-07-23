[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.Star2.Lib

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory

let private slopesOfInterest =
    [ XY(1, 1); XY(3, 1); XY(5, 1); XY(7, 1); XY(1, 2) ]


let countCollisions forest =

    let countCollisionsOn slope =
        forest
        |> Forest.tilesOn slope
        |> List.countItem Tree
        |> int64

    slopesOfInterest |> List.map countCollisionsOn |> List.mult
