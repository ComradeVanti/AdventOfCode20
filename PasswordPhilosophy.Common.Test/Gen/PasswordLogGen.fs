module AdventOfCode20.PasswordPhilosophy.PasswordLogGen

open AdventOfCode20
open AdventOfCode20.CharGen
open AdventOfCode20.PasswordPhilosophy
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open AdventOfCode20.PasswordPhilosophy.PasswordStructureGen
open FsCheck
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

type MatchingDay1 = MatchingDay1 of PasswordLog
type MatchingDay2 = MatchingDay2 of PasswordLog
type MatchingNeither = MatchingNeither of PasswordLog
type MatchingBoth = MatchingBoth of PasswordLog


let private requiredIndicesFor policy =
    Set.ofList [ policy.MinCount - 1; policy.MaxCount - 1 ]

let private genPasswordWith (structure: PasswordStructure) policyLetter =
    let genPolicyLetter = Gen.constant policyLetter
    let genNonPolicyLetter = genLetter |> Gen.except policyLetter

    structure
    |> List.map (fun isPolicyLetter ->
        if isPolicyLetter then
            genPolicyLetter
        else
            genNonPolicyLetter)
    |> Gen.sequence
    |> Gen.map System.String.Concat

let private genLog matchingDay1 matchingDay2 =
    gen {
        let! policy = genPolicy
        let! structure = genStructureFor policy matchingDay1 matchingDay2
        let! password = genPasswordWith structure policy.Letter
        return { Password = password; Policy = policy }
    }

let genMatchingDay1Log = genLog true false

let genMatchingDay2Log = genLog false true

let genMatchingBothLog = genLog true true

let genMatchingNeitherLog = genLog false false

let genPasswordLog =
    Gen.oneof [ genMatchingDay1Log
                genMatchingDay2Log
                genMatchingBothLog
                genMatchingNeitherLog ]

type ArbPasswordLogs =
    static member Mixed() = Arb.fromGen genPasswordLog

    static member Day1() =
        Arb.fromGen (genMatchingDay1Log |> Gen.map MatchingDay1)

    static member Day2() =
        Arb.fromGen (genMatchingDay2Log |> Gen.map MatchingDay2)

    static member Both() =
        Arb.fromGen (genMatchingBothLog |> Gen.map MatchingBoth)

    static member Neither() =
        Arb.fromGen (genMatchingNeitherLog |> Gen.map MatchingNeither)
