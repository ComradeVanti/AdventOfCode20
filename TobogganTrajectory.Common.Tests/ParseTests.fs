namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20.Props
open AdventOfCode20.TobogganTrajectory.ForestGen
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbForests> |])>]
module ParseTests =

    let private stringify forest =

        let stringifyTile tile =
            match tile with
            | Tree -> '#'
            | Empty -> '.'

        let stringifyRow row =
            row |> List.map stringifyTile |> System.String.Concat

        Forest.rowsOf forest |> List.map stringifyRow

    [<Property(MaxTest = 5)>]
    let ``Forests are parsed correctly`` forest =
        let lines = stringify forest

        match Parse.forest lines with
        | Some parsed -> parsed =? forest
        | None -> rejectWith "Could not parse"
