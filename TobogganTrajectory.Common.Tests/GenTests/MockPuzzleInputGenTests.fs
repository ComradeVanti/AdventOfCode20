namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen
open AdventOfCode20.TobogganTrajectory.ForestMapGenTests
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbPuzzleInputs> |])>]
module MockPuzzleInputGenTests =

    [<Property>]
    let ``Forest-map is valid`` input = input.ForestMap |> mapIsValid
