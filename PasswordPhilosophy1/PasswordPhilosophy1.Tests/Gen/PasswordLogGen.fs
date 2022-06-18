module PasswordPhilosophy1.PasswordGen

open FsCheck
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
        let! log = getLogWithLetterCount policy letterCount
        return ValidLog log
    }

let genInvalidPasswordFor (policy: Policy) =
    gen {
        let! letterCount =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        Gen.choose (policy.MaxCount + 1, policy.MaxCount + 5) ]

        let! log = getLogWithLetterCount policy letterCount
        return InvalidLog log
    }

let genValidPassword =
    gen {
        let! policy = genPolicy
        return! genValidPasswordFor policy
    }

let genInvalidPassword =
    gen {
        let! policy = genPolicy
        return! genInvalidPasswordFor policy
    }


type ArbPasswordLogs =
    static member Valid() = Arb.fromGen genValidPassword
    static member Invalid() = Arb.fromGen genInvalidPassword
