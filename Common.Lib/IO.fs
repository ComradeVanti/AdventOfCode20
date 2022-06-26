[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.IO

open System.IO

let tryReadLines path =
    try
        File.ReadAllLines path |> List.ofArray |> Some
    with
    | _ -> None
