[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Prop

open FsCheck
open Microsoft.FSharp.Collections

let all props =
    match props with
    | [] -> Prop.ofTestable true
    | _ -> props |> List.reduce (.&.)
