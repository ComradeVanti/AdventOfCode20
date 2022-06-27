[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Seq

let countWith f seq = seq |> Seq.filter f |> Seq.length
