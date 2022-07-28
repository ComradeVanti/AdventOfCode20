[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.List

let countWith f list = list |> List.filter f |> List.length

let countItem i = countWith ((=) i)

let inline mult list = list |> List.reduce (*)

let indices list = List.init (list |> List.length) id

let pairs items =
    let length = items |> List.length

    seq {
        for i in [ 0 .. length - 1 ] do
            let first = items |> List.item i

            for o in [ i + 1 .. length - 1 ] do
                let second = items |> List.item o
                yield (first, second)
    }

let triplets items =
    let length = items |> List.length

    seq {
        for i in [ 0 .. length - 1 ] do
            let first = items |> List.item i

            for o in [ i + 1 .. length - 1 ] do
                let second = items |> List.item o

                for p in [ o + 1 .. length - 1 ] do
                    let third = items |> List.item p
                    yield (first, second, third)
    }

let allEqual list = list |> List.distinct |> List.length <= 1

let mapAt i f list =
    list
    |> List.mapi (fun index item -> if index = i then f item else item)

let isDistinct list = list |> List.distinct = list

let tryIndexOf item list = list |> List.tryFindIndex ((=) item)

let splitAtItem item list =

    let rec split sublist =
        match sublist |> tryIndexOf item with
        | Some index ->
            let chunk, rest = sublist |> List.splitAt index
            chunk :: (split (rest |> List.skip 1))
        | None -> [ sublist ]

    split list
