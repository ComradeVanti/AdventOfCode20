[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Parse

open System

let int s =
    try
        Int32.Parse s |> Some
    with
    | _ -> None
