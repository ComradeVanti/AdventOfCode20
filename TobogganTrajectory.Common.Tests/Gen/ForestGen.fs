module AdventOfCode20.TobogganTrajectory.ForestGen

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory
open AdventOfCode20.TobogganTrajectory.CollisionCountsGen
open FsCheck
open Microsoft.FSharp.Collections

type private Properties = { Size: int * int; FixedTiles: Map<V2<int>, Tile> }

let private baseFixedTiles = Map [ (XY(0, 0), Empty) ]

let private minHeightFor collisions =

    let minRowCountFor (slope, count) =
        let y = V2.yOf slope
        (count * y) + 1

    collisions
    |> Map.toList
    |> List.map minRowCountFor
    |> List.max
    |> (*) 2

let private genSizeFor collisions =
    gen {
        let minHeight = minHeightFor collisions
        let! height = Gen.choose (minHeight, minHeight * 2)
        let! width = Gen.choose (20, 30)
        return (width, height)
    }

let private fixEmptyIn fixedTiles pos = fixedTiles |> Map.safeAdd pos Empty

let private fixTreeAt pos fixedTiles = fixedTiles |> Map.safeAdd pos Tree

let private isFixedIn fixedTiles pos = fixedTiles |> Map.containsKey pos

let private isTreeIn fixedTiles pos =
    fixedTiles |> Map.tryFind pos |> Option.contains Tree

let private genFixedTilesFor collisions size =

    let slopes = collisions |> Map.keys |> Set.ofSeq

    let slopePositions =
        slopes
        |> Set.map (fun slope ->
            let positions =
                Forest.positionsOn slope
                |> Forest.inForestOfSize size
                |> Set.ofList

            (slope, positions))
        |> Map.ofSeq

    let countFor slope = collisions |> Map.find slope

    let positionsOn slope = slopePositions |> Map.find slope

    let isOn slope pos = positionsOn slope |> Set.contains pos

    let presentTreeCountOn slope fixedTiles =
        positionsOn slope
        |> Set.filter (isTreeIn fixedTiles)
        |> Set.count

    let missingTreeCountOn slope fixedTiles =
        let presentTreeCount = fixedTiles |> presentTreeCountOn slope
        (countFor slope) - presentTreeCount

    let addTreeOn slope fixedTiles =

        let canFitMoreTreesOn slope =
            let missingTreeCount = fixedTiles |> missingTreeCountOn slope
            missingTreeCount > 0

        let isValidFor slope pos =
            (not (pos |> isOn slope)) || canFitMoreTreesOn slope

        let isValidForAllSlopes pos =
            slopes |> Set.forall (fun slope -> pos |> isValidFor slope)

        let freePositions =
            positionsOn slope
            |> Set.filter (not << isFixedIn fixedTiles)

        let possiblePositions =
            freePositions |> Set.filter isValidForAllSlopes

        gen {
            let! position = Gen.elements possiblePositions

            return fixedTiles |> fixTreeAt position
        }

    let fixEmptyPositionsFor slope fixedTiles =
        positionsOn slope
        |> Set.filter (not << isFixedIn fixedTiles)
        |> Set.fold fixEmptyIn fixedTiles

    let addFixedTilesTo fixedTiles slope =

        let missingTreeCount = fixedTiles |> missingTreeCountOn slope

        gen {
            let! withTrees =
                Gen.foldn (addTreeOn slope) fixedTiles missingTreeCount

            return withTrees |> fixEmptyPositionsFor slope
        }

    slopes
    |> Set.toList
    |> Gen.fold addFixedTilesTo baseFixedTiles

let private genPropertiesFor collisions =
    gen {
        let! size = genSizeFor collisions
        let! fixedTiles = genFixedTilesFor collisions size
        return { Size = size; FixedTiles = fixedTiles }
    }

let private genForestWith properties =
    let width, height = properties.Size

    let tryGetFixedTileAt pos = properties.FixedTiles |> Map.tryFind pos

    let genTileAt pos =
        match tryGetFixedTileAt pos with
        | Some tile -> Gen.constant tile
        | None -> Gen.elements [ Tree; Empty ]

    let genRow y = Gen.initList width (fun x -> genTileAt (XY(x, y)))

    let genTiles = Gen.initList height genRow

    genTiles |> Gen.map Forest.fromTiles

let genForestWithCollisions collisions =
    gen {
        let! properties = genPropertiesFor collisions
        return! genForestWith properties
    }

let genForest =
    gen {
        let! collisions = genCollisions
        return! genForestWithCollisions collisions
    }

type ArbForests =
    static member Default() = Arb.fromGen genForest
