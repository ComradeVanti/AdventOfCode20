[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.PasswordPhilosophy.Parse

open AdventOfCode20

let private parseCounts s =
    match s |> String.splitAt '-' with
    | [ minPart; maxPart ] ->
        opt {
            let! min = Parse.int minPart
            let! max = Parse.int maxPart
            return (min, max)
        }
    | _ -> None

let private parsePolicy s =
    match s |> String.splitAt ' ' with
    | [ countPart; letterPart ] ->
        opt {
            let! minCount, maxCount = parseCounts countPart
            let letter = char letterPart

            return
                { MinCount = minCount
                  MaxCount = maxCount
                  Letter = letter }
        }
    | _ -> None

let log s =
    match s |> String.splitAt ':' with
    | [ policyPart; passwordPart ] ->
        opt {
            let! policy = parsePolicy policyPart
            let password = passwordPart |> String.applySeqF (Seq.skip 1)
            return { Password = password; Policy = policy }
        }
    | _ -> None

let passwordReport lines =
    lines |> List.map log |> Option.collect |> Option.map Logs
