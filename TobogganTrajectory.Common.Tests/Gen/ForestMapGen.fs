module AdventOfCode20.TobogganTrajectory.ForestMapGen

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory
open FsCheck

let private genTile = Gen.elements [ Empty; Tree ]

let private genRowWithLength length = Gen.listOfLength length genTile

[<Literal>]
let private MinWidth = 3

[<Literal>]
let private MaxWidth = 20

[<Literal>]
let private MinHeight = 2

[<Literal>]
let private MaxHeight = 200

let private genWidth = Gen.choose (MinWidth, MaxWidth)

let private genHeight = Gen.choose (MinHeight, MaxHeight)

let private genMapSize =
    gen {
        let! width = genWidth
        let! height = genHeight
        return (width, height)
    }

let private genRow width = Gen.listOfLength width genTile

let private genForestMapWithSize (width, height) =
    Gen.listOfLength height (genRow width) |> Gen.map Tiles

let genForestMapAndCollisionCount =
    gen {
        let! width, height = genMapSize

        let rec buildRows position index =
            gen {
                let! row = genRow width
                let row =
                    if index = 0 then row |> List.updateAt 0 Empty else row

                let hasTree =
                    row
                    |> List.tryItem (position % width)
                    |> Option.contains Tree
                let count = if hasTree then 1 else 0

                if index < height then
                    let! restRows, restCount =
                        buildRows (position + 3) (index + 1)

                    return (row :: restRows, count + restCount)
                else
                    return ([ row ], count)
            }

        let! tiles, count = buildRows 0 0
        return (Tiles tiles, count)
    }

let genForestMap = genForestMapAndCollisionCount |> Gen.map fst

type ArbForestMaps =
    static member Default() = Arb.fromGen genForestMap
