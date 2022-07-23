module AdventOfCode20.TobogganTrajectory.CollisionCountsGen

open AdventOfCode20
open FsCheck


let genCollisionsBelow cap =
    let genCount = Gen.choose (0, cap - 1)

    let genForSlope slope =
        gen {
            let! count = genCount
            return (slope, count)
        }

    slopesOfInterest
    |> List.map genForSlope
    |> Gen.sequence
    |> Gen.map Map

let genCollisions =
    Gen.sized (fun s ->
        let cap = min s 15
        genCollisionsBelow cap)
