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

let rec repeatWhile pred seed g =
    if not (seed |> pred) then
        Gen.constant seed
    else
        gen {
            let! value = g seed
            return! repeatWhile pred value g
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

let exceptAllIn items g =
    g
    |> Gen.filter (fun value -> not <| (items |> Seq.contains value))

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

let setWithCount count g =

    let growSet set =
        gen {
            let! value = g
            return set |> Set.add value
        }

    repeatWhile (fun set -> set |> Set.count < count) Set.empty growSet

let initList length f =

    let rec initListAt index =
        if index = length then
            Gen.constant []
        else
            gen {
                let! head = f index
                let! tail = initListAt (index + 1)
                return head :: tail
            }
        
    initListAt 0