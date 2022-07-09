namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory.ForestMapGen
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbForestMaps> |])>]
module ForestMapGenTests =

    [<Property>]
    let ``Map is at least 3x2`` map =
        let width = ForestMap.widthOf map
        let height = ForestMap.heightOf map

        let widthCorrect = width >= 3 |@ $"Width too small (%i{width})"

        let heightCorrect =
            height >= 2 |@ $"Height too small (%i{height})"

        widthCorrect .&. heightCorrect

    [<Property>]
    let ``All rows have same length`` map =
        ForestMap.rowsOf map
        |> List.map List.length
        |> List.allEqual

    [<Property>]
    let ``Top-left is always empty`` map =
        let tile = ForestMap.rowsOf map |> List.head |> List.head

        match tile with
        | Empty -> true
        | _ -> false

    let mapIsValid map =
        (map |> ``Map is at least 3x2``)
        .&. (map |> ``All rows have same length``)
        .&. (map |> ``Top-left is always empty``)
