[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.String

let repeatChar times (c: char) = Seq.replicate times c |> System.String.Concat

let applySeqF (mapF: seq<char> -> seq<char>) (s: string) =
    s |> mapF |> System.String.Concat

let splitAt (c: char) (s: string) = s.Split c |> List.ofArray
