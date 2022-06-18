[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module PasswordPhilosophy1.Gen

open FsCheck
open Microsoft.FSharp.Collections

let indexOf list = Gen.choose (0, (list |> List.length) - 1)

let rec randomized list =
    if list |> List.isEmpty then
        Gen.constant []
    else
        gen {
            let! index = indexOf list
            let head = list |> List.item index
            let! tail = randomized (list |> List.removeAt index)
            return head :: tail
        }
