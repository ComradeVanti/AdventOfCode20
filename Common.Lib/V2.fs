namespace AdventOfCode20

open Microsoft.FSharp.Core

type V2<'a> = XY of 'a * 'a

[<RequireQualifiedAccess>]
module V2 =

    let xyOf (XY (x, y)) = (x, y)

    let xOf v = xyOf v |> fst

    let yOf v = xyOf v |> snd

    let inline length v =
        let x, y = xyOf v
        sqrt (x * x + y * y)

    let inline add other v =
        let x1, y1 = xyOf v
        let x2, y2 = xyOf other
        XY(x1 + x2, y1 + y2)
