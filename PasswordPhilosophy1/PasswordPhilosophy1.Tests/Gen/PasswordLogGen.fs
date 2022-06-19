module PasswordPhilosophy1.PasswordGen

open FsCheck
open TestsCommon
open PasswordPhilosophy1
open PasswordPhilosophy1.PolicyGen

type ValidLog = ValidLog of PasswordLog
type InvalidLog = InvalidLog of PasswordLog

let genLetterExcept c = genLetter |> Gen.filter ((<>) c)

let genPassword policyLetter policyCharCount nonPolicyCharCount =
    gen {
        let policyLetters =
            (List.replicate policyCharCount policyLetter)

        let! nonPolicyLetters =
            Gen.listOfLength nonPolicyCharCount (genLetterExcept policyLetter)

        return!
            Gen.randomized (policyLetters @ nonPolicyLetters)
            |> Gen.map System.String.Concat
    }

let getLogWithLetterCount (policy: Policy) count =
    gen {
        let! otherCharCount = Gen.choose (1, 10)
        let! password = genPassword policy.Character count otherCharCount
        return PasswordLog(password, policy)
    }

let genValidPasswordFor (policy: Policy) =
    gen {
        let! letterCount = Gen.choose (policy.MinCount, policy.MaxCount)
        return! getLogWithLetterCount policy letterCount
    }

let genInvalidPasswordFor (policy: Policy) =
    gen {
        let! letterCount =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        Gen.choose (policy.MaxCount + 1, policy.MaxCount + 5) ]

        return! getLogWithLetterCount policy letterCount
    }

let genValidLog =
    gen {
        let! policy = genPolicy
        return! genValidPasswordFor policy
    }

let genInvalidLog =
    gen {
        let! policy = genPolicy
        return! genInvalidPasswordFor policy
    }

let genLog = Gen.oneof [ genValidLog; genInvalidLog ]

type ArbPasswordLogs =
    static member Valid() = Arb.fromGen (genValidLog |> Gen.map ValidLog)
    static member Invalid() = Arb.fromGen (genInvalidLog |> Gen.map InvalidLog)
    static member Default() = Arb.fromGen genLog
