[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.Domain

type Tile =
    | Empty
    | Tree
    
type ForestMap = Tiles of Tile list list