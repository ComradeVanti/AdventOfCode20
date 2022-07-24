[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Option

let rec collect opts =
    match opts with
    | [] -> Some []
    | head :: tail -> Option.map2 (fun h t -> h :: t) head (collect tail)
