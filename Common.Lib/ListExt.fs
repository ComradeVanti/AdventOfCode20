[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.List

let countWith f list = list |> List.filter f |> List.length
