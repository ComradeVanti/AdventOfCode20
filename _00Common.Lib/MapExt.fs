[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Map

open Microsoft.FSharp.Collections

let safeAdd key value map =
    if map |> Map.containsKey key then
        map
    else
        map |> Map.add key value
