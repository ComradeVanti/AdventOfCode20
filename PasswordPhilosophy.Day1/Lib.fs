[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.PasswordPhilosophy.Day1.Lib

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy

let matches policy password =
    let letterCount = password |> String.countChar policy.Letter

    letterCount >= policy.MinCount
    && letterCount <= policy.MaxCount

let private isValid log = log.Password |> matches log.Policy

let countValid report =
    let (Logs logs) = report
    logs |> Seq.countWhere isValid
