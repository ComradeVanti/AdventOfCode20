module AdventOfCode20.PasswordPhilosophy.PasswordLogGen

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open AdventOfCode20.CharGen
open FsCheck
open Microsoft.FSharp.Collections
open Microsoft.FSharp.Core

type MatchingDay1 = MatchingDay1 of PasswordLog
type MatchingDay2 = MatchingDay2 of PasswordLog
type MatchingNeither = MatchingNeither of PasswordLog
type MatchingBoth = MatchingBoth of PasswordLog

[<Literal>]
let private MaxPasswordLength = 20

let private requiredIndicesFor policy =
    Set.ofList [ policy.MinCount - 1; policy.MaxCount - 1 ]

let private genPassword
    length
    policyLetterCount
    requiredIndices
    forbiddenIndices
    policyLetter
    =
    gen {
        let requiredCount = requiredIndices |> Set.count

        let genIndex = Gen.choose (0, length - 1)
        let genPolicyLetter = Gen.constant policyLetter
        let genNonPolicyLetter = genLetter |> Gen.except policyLetter

        let! policyLetterIndices =
            genIndex
            |> Gen.exceptAllIn requiredIndices
            |> Gen.exceptAllIn forbiddenIndices
            |> Gen.setWithCount (policyLetterCount - requiredCount)
            |> Gen.map (Set.union requiredIndices)

        return!
            Gen.initList length (fun index ->
                if policyLetterIndices |> Set.contains index then
                    genPolicyLetter
                else
                    genNonPolicyLetter)
            |> Gen.map System.String.Concat
    }

let private genMatchingDay1For policy =
    gen {
        let forbiddenIndices = requiredIndicesFor policy
        let! policyLetterCount = Gen.choose (policy.MinCount, policy.MaxCount)

        let! length =
            Gen.choose (
                max (policyLetterCount + 2) MinPasswordLength,
                MaxPasswordLength
            )

        return!
            genPassword
                length
                policyLetterCount
                Set.empty
                forbiddenIndices
                policy.Letter
    }

let private genMatchingDay2For policy =
    gen {
        let! policyLetterCount =
            Gen.oneof [ if policy.MinCount > 2 then
                            Gen.choose (2, policy.MinCount - 1)
                        Gen.choose (policy.MaxCount + 1, MaxPasswordLength) ]

        let! length =
            Gen.choose (
                max policy.MaxCount policyLetterCount,
                MaxPasswordLength
            )

        return!
            genPassword
                length
                policyLetterCount
                (requiredIndicesFor policy)
                Set.empty
                policy.Letter
    }

let private genMatchingNeitherFor policy =
    gen {
        let forbiddenIndices = requiredIndicesFor policy
        let! length = Gen.choose (MinPasswordLength, MaxPasswordLength)

        let! policyLetterCount =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        if length > policy.MaxCount + 2 then
                            Gen.choose (policy.MaxCount + 1, length - 2) ]

        return!
            genPassword
                length
                policyLetterCount
                Set.empty
                forbiddenIndices
                policy.Letter
    }

let private genMatchingBothFor policy =
    gen {
        let! policyLetterCount = Gen.choose (policy.MinCount, policy.MaxCount)

        let! length =
            Gen.choose (
                List.max [ (policyLetterCount + 2)
                           policy.MaxCount
                           MinPasswordLength ],
                MaxPasswordLength
            )

        return!
            genPassword
                length
                policyLetterCount
                (requiredIndicesFor policy)
                Set.empty
                policy.Letter
    }

let private genLogWith f =
    gen {
        let! policy = genPolicy
        let! password = f policy
        return { Password = password; Policy = policy }
    }

let private genValidPasswordFor policy =
    gen {
        let minLength = policy.MaxCount + 1
        let maxLength = minLength + 10

        let requiredIndices =
            Set.ofList [ policy.MinCount - 1; policy.MaxCount - 1 ]

        let! length = Gen.choose (minLength, maxLength)
        let! policyLetterCount = Gen.choose (policy.MinCount, policy.MaxCount)

        let genIndex = Gen.choose (0, length - 1)
        let genPolicyLetter = Gen.constant policy.Letter
        let genNonPolicyLetter = genLetter |> Gen.except policy.Letter

        let! policyLetterIndices =
            genIndex
            |> Gen.exceptAllIn requiredIndices
            |> Gen.setWithCount (policyLetterCount - 2)
            |> Gen.map (Set.union requiredIndices)

        return!
            Gen.initList length (fun index ->
                if policyLetterIndices |> Set.contains index then
                    genPolicyLetter
                else
                    genNonPolicyLetter)
            |> Gen.map System.String.Concat
    }

let genMatchingDay1Log = genLogWith genMatchingDay1For

let genMatchingDay2Log = genLogWith genMatchingDay2For

let genMatchingBothLog = genLogWith genMatchingBothFor

let genMatchingNeitherLog = genLogWith genMatchingNeitherFor

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
