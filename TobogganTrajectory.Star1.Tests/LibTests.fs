namespace AdventOfCode20.TobogganTrajectory.Star1

open AdventOfCode20
open AdventOfCode20.Props
open AdventOfCode20.TobogganTrajectory
open AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPuzzleInputs> |])>]
module LibTests =

    [<Property>]
    let ``Finds correct collision-count`` input =
        let x3y1Count = input.Collisions |> Map.find (XY(3, 1))
        (countCollisions input.Forest) =? x3y1Count
