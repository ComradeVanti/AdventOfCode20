[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Gen

open FsCheck

let rec repeat seed count g =
    if count = 0 then
        Gen.constant seed
    else
        gen {
            let! value = g seed
            return! repeat value (count - 1) g
        }

let indexIn list =
    match list with
    | [] -> Gen.constant 0
    | _ ->
        gen {
            let length = list |> List.length
            return! Gen.choose (0, length - 1)
        }

let except item g = g |> Gen.filter ((<>) item)

let rec shuffledList list =
    match list with
    | [] -> Gen.constant []
    | _ ->
        gen {
            let! index = indexIn list
            let item = list |> List.item index
            let! rest = list |> List.removeAt index |> shuffledList
            return item :: rest
        }
