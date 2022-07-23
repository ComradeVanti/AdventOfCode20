namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20

type Forest = private TileGrid of Grid<Tile>


[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module Forest =

    let fromTiles = Grid.make >> TileGrid

    let private gridOf (TileGrid grid) = grid

    let rowsOf forest = Grid.rowsOf (gridOf forest)

    let widthOf forest = Grid.widthOf (gridOf forest)

    let heightOf forest = Grid.heightOf (gridOf forest)

    let mapAt i f forest = (gridOf forest) |> Grid.mapAt i f |> TileGrid

    let updateAt i tile forest =
        (gridOf forest) |> Grid.updateAt i tile |> TileGrid

    let tryTile i forest =
        let x, y = V2.xyOf i
        let width = widthOf forest
        let repeated = XY(x % width, y)

        gridOf forest |> Grid.tryItem repeated

    let tile i forest = forest |> tryTile i |> Option.get

    let hasTreeAt i forest = forest |> tryTile i |> Option.contains Tree

    let positionsOn slope forest =

        let width, height = widthOf forest, heightOf forest

        let rec positionsStartingAt pos =
            if V2.yOf pos = height then
                []
            else
                let nextPos = pos |> V2.add slope
                pos :: (positionsStartingAt nextPos)

        positionsStartingAt (XY(0, 0))

    let tilesOn slope forest =
        forest
        |> positionsOn slope
        |> List.map (fun pos -> forest |> tile pos)
