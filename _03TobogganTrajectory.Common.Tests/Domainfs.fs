[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.TobogganTrajectory.TestDomain

open AdventOfCode20

type Slope = V2<int>

type MockPuzzleInput = { Forest: Forest; Collisions: Map<Slope, int> }

let slopesOfInterest =
    [ XY(1, 1); XY(3, 1); XY(5, 1); XY(7, 1); XY(1, 2) ]
