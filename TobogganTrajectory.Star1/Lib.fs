[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.Star1.Lib

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory

let countCollisions forest =
    forest |> Forest.tilesOn (XY(3, 1)) |> List.countItem Tree
