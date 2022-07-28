[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.String

open System

let repeatChar times (c: char) = Seq.replicate times c |> System.String.Concat

let applySeqF (mapF: seq<char> -> seq<char>) (s: string) =
    s |> mapF |> System.String.Concat

let splitAt (c: char) (s: string) = s.Split c |> List.ofArray

let countChar (c: char) (s: string) = s |> Seq.countItem c

let charAt index (s: string) = s |> Seq.item index

let tryCharAt index (s: string) = s |> Seq.tryItem index

let joinWith (c: char) (strings: string list) = String.Join(c, strings)
