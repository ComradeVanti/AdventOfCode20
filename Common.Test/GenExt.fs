[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.Gen

open FsCheck

let rec repeat seed count g =
    if count = 0 then
        Gen.constant seed
    else
        gen {
            let! value = g seed
            return! repeat value (count - 1) g
        }

let indexIn list =
    gen {
        let length = list |> List.length
        return! Gen.choose (0, length - 1)
    }
