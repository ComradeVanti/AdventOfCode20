namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20.Props
open AdventOfCode20.TobogganTrajectory.ForestMapGen
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbForestMaps> |])>]
module ParseTests =

    let private stringify map =

        let stringifyTile tile =
            match tile with
            | Tree -> '#'
            | Empty -> '.'

        let stringifyRow row =
            row |> List.map stringifyTile |> System.String.Concat

        ForestMap.rowsOf map |> List.map stringifyRow

    [<Property>]
    let ``Maps are parsed correctly`` map =
        let lines = stringify map

        match Parse.forestMap lines with
        | Some parsed -> parsed =? map
        | None -> rejectWith "Could not parse"
