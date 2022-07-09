[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.Domain

type Tile =
    | Empty
    | Tree

(*
    [
        [ Empty; Empty; Empty ]
        [ Empty; Empty; Empty ]
        [ Empty; Empty; Empty ]
        [ Empty; Empty; Empty ]
    ]
*)
type ForestMap = Tiles of Tile list list
