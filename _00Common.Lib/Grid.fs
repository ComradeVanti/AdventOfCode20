namespace AdventOfCode20

open AdventOfCode20

type Grid<'a> = private Rows of 'a list list

[<RequireQualifiedAccess>]
module Grid =

    let tryMake rows =
        if rows |> List.map List.length |> List.allEqual then
            Some(Rows rows)
        else
            None

    let make rows = rows |> tryMake |> Option.get

    let rowsOf (Rows rows) = rows

    let widthOf grid =
        rowsOf grid
        |> List.tryHead
        |> Option.map List.length
        |> Option.defaultValue 0

    let heightOf grid = rowsOf grid |> List.length

    let mapAt i f grid =
        let x, y = V2.xyOf i

        rowsOf grid
        |> List.mapAt y (List.mapAt x f)
        |> Rows

    let updateAt i item grid = grid |> mapAt i (fun _ -> item)

    let tryItem i grid =

        let x, y = V2.xyOf i

        rowsOf grid
        |> List.tryItem y
        |> Option.bind (List.tryItem x)
