namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory.ForestGen
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbForests> |])>]
module ForestGenTests =

    [<Property>]
    let ``Forest is at least 3x2`` forest =
        let width = Forest.widthOf forest
        let height = Forest.heightOf forest

        let widthCorrect = width >= 3 |@ $"Width too small (%i{width})"

        let heightCorrect =
            height >= 2 |@ $"Height too small (%i{height})"

        widthCorrect .&. heightCorrect

    [<Property>]
    let ``Top-left is always empty`` forest =
        let tile = forest |> Forest.tile (XY(0, 0))

        match tile with
        | Empty -> true
        | _ -> false

    let mapIsValid forest =
        (forest |> ``Forest is at least 3x2``)
        .&. (forest |> ``Top-left is always empty``)
