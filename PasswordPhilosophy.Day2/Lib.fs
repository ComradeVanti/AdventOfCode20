[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.PasswordPhilosophy.Day2.Lib

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy

let matches policy password =

    let hasLetterAt index =
        password
        |> String.tryCharAt index
        |> Option.contains policy.Letter

    let hasLetterA = hasLetterAt (policy.MinCount - 1)
    let hasLetterB = hasLetterAt (policy.MaxCount - 1)

    hasLetterA ||! hasLetterB

let private isValid log = log.Password |> matches log.Policy

let countValid report =
    let (Logs logs) = report
    logs |> Seq.countWhere isValid
