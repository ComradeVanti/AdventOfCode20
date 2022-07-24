namespace AdventOfCode20.TobogganTrajectory.Star2

open AdventOfCode20
open AdventOfCode20.Props
open AdventOfCode20.TobogganTrajectory
open AdventOfCode20.TobogganTrajectory.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPuzzleInputs> |])>]
module LibTests =

    let totalCount input =

        let countFor slope = int64 (input.Collisions |> Map.find slope)

        let x1y1Count = countFor (XY(1, 1))
        let x3y1Count = countFor (XY(3, 1))
        let x5y1Count = countFor (XY(5, 1))
        let x7y1Count = countFor (XY(7, 1))
        let x1y2Count = countFor (XY(1, 2))

        x1y1Count * x3y1Count * x5y1Count * x7y1Count * x1y2Count

    [<Property>]
    let ``Finds correct collision-count`` input =
        let totalCount = input |> totalCount
        (countCollisions input.Forest) =? totalCount
