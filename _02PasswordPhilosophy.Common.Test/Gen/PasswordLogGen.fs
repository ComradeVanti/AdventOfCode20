module AdventOfCode20.PasswordPhilosophy.PasswordLogGen

open AdventOfCode20
open AdventOfCode20.CharGen
open AdventOfCode20.PasswordPhilosophy
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open AdventOfCode20.PasswordPhilosophy.PasswordStructureGen
open FsCheck
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

type MatchingStar1 = MatchingStar1 of PasswordLog
type MatchingStar2 = MatchingStar2 of PasswordLog
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

let private genLog matchingStar1 matchingStar2 =
    gen {
        let! policy = genPolicy
        let! structure = genStructureFor policy matchingStar1 matchingStar2
        let! password = genPasswordWith structure policy.Letter
        return { Password = password; Policy = policy }
    }

let genMatchingStar1Log = genLog true false

let genMatchingStar2Log = genLog false true

let genMatchingBothLog = genLog true true

let genMatchingNeitherLog = genLog false false

let genPasswordLog =
    Gen.oneof [ genMatchingStar1Log
                genMatchingStar2Log
                genMatchingBothLog
                genMatchingNeitherLog ]

type ArbPasswordLogs =
    static member Mixed() = Arb.fromGen genPasswordLog

    static member Star1() =
        Arb.fromGen (genMatchingStar1Log |> Gen.map MatchingStar1)

    static member Star2() =
        Arb.fromGen (genMatchingStar2Log |> Gen.map MatchingStar2)

    static member Both() =
        Arb.fromGen (genMatchingBothLog |> Gen.map MatchingBoth)

    static member Neither() =
        Arb.fromGen (genMatchingNeitherLog |> Gen.map MatchingNeither)
