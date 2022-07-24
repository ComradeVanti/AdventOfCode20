[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Seq

let countWhere f seq = seq |> Seq.filter f |> Seq.length

let countItem item seq = seq |> countWhere ((=) item)
