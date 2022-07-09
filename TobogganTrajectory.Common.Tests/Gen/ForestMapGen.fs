module AdventOfCode20.TobogganTrajectory.ForestMapGen

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

let private genForestMapWithSize (width, height) =
    let genRow = Gen.listOfLength width genTile
    Gen.listOfLength height genRow |> Gen.map Tiles

let genForestMap =
    gen {
        let! mapSize = genMapSize
        let! forestMap = genForestMapWithSize mapSize
        return forestMap |> ForestMap.setTileAt (0, 0) Empty
    }

type ArbForestMaps =
    static member Default() = Arb.fromGen genForestMap
