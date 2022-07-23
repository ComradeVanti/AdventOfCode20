namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20
open AdventOfCode20.TobogganTrajectory

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

    let inForestOfSize size f = f size

    let inForest forest f =
        let size = widthOf forest, heightOf forest
        inForestOfSize size f

    let repeat i =
        (fun (width, _) ->
            let x, y = V2.xyOf i
            XY(x % width, y))

    let tryTile i forest =
        let repeated = repeat i |> (inForest forest)
        gridOf forest |> Grid.tryItem repeated

    let tile i forest = forest |> tryTile i |> Option.get

    let hasTreeAt i forest = forest |> tryTile i |> Option.contains Tree

    let positionsOn slope =
        (fun size ->

            let _, height = size

            let rec positionsStartingAt pos =
                if V2.yOf pos >= height then
                    []
                else
                    let nextPos =
                        pos |> V2.add slope |> repeat |> (inForestOfSize size)

                    pos :: (positionsStartingAt nextPos)

            positionsStartingAt (XY(0, 0)))

    let tilesOn slope forest =
        positionsOn slope
        |> inForest forest
        |> List.map (fun pos -> forest |> tile pos)
