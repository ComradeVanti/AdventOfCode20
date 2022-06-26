[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.List

let countWith f list = list |> List.filter f |> List.length

let mult list = list |> List.reduce (*)

let indices list = List.init (list |> List.length) id
