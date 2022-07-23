module AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen

open AdventOfCode20.TobogganTrajectory.CollisionCountsGen
open AdventOfCode20.TobogganTrajectory.ForestGen
open FsCheck

let genMockPuzzleInput =
    gen {
        let! collisions = genCollisions
        let! forest = genForestWithCollisions collisions
        return { Forest = forest; Collisions = collisions }
    }

type ArbPuzzleInputs =
    static member Default() = Arb.fromGen genMockPuzzleInput
