module AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen

open AdventOfCode20.TobogganTrajectory.ForestMapGen
open FsCheck

let genMockPuzzleInput =
    gen {
        let! forestMap, count = genForestMapAndCollisionCount
        return { ForestMap = forestMap; CollisionCount = count }
    }

type ArbPuzzleInputs =
    static member Default() = Arb.fromGen genMockPuzzleInput
