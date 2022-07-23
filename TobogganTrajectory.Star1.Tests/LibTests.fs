namespace AdventOfCode20.TobogganTrajectory.Star1

open AdventOfCode20.Props
open AdventOfCode20.TobogganTrajectory
open AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPuzzleInputs> |])>]
module LibTests =

    [<Property>]
    let ``Finds correct collision-count`` input =
        (countCollisions input.Forest) =? input.CollisionCount
