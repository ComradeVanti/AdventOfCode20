module AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory.ForestGen
open FsCheck

let genMockPuzzleInput =
    gen {
        let! forest, count = genForestAndCollisionCount
        let collisionCounts = Map [ (XY(3, 1), count) ]
        return { Forest = forest; Collisions = collisionCounts }
    }

type ArbPuzzleInputs =
    static member Default() = Arb.fromGen genMockPuzzleInput
