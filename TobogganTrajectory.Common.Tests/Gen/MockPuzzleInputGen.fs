module AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen

open AdventOfCode20.TobogganTrajectory.ForestGen
open FsCheck

let genMockPuzzleInput =
    gen {
        let! forest, count = genForestAndCollisionCount
        return { Forest = forest; CollisionCount = count }
    }

type ArbPuzzleInputs =
    static member Default() = Arb.fromGen genMockPuzzleInput
