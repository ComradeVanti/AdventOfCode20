namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen
open AdventOfCode20.TobogganTrajectory.ForestGenTests
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbPuzzleInputs> |])>]
module MockPuzzleInputGenTests =

    [<Property>]
    let ``Forest is valid`` input = input.Forest |> mapIsValid
