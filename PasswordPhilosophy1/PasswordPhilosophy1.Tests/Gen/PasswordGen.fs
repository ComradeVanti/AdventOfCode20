module PasswordPhilosophy1.PasswordGen

open FsCheck
open PasswordPhilosophy1.PolicyGen

type ValidPasswordWithPolicy = ValidPasswordWithPolicy of (string * Policy)
type InvalidPasswordWithPolicy = InvalidPasswordWithPolicy of (string * Policy)

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

let genPasswordWithLetter letter count =
    gen {
        let! otherCharCount = Gen.choose (1, 10)

        return! genPassword letter count otherCharCount
    }


let genValidPasswordFor (policy: Policy) =
    gen {
        let! letterCount = Gen.choose (policy.MinCount, policy.MaxCount)
        let! password = genPasswordWithLetter policy.Character letterCount

        return ValidPasswordWithPolicy(password, policy)
    }

let genInvalidPasswordFor (policy: Policy) =
    gen {
        let! letterCount =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        Gen.choose (policy.MaxCount + 1, policy.MaxCount + 5) ]

        let! password = genPasswordWithLetter policy.Character letterCount

        return InvalidPasswordWithPolicy(password, policy)
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


type ArbPasswords =
    static member Valid() = Arb.fromGen genValidPassword
    static member Invalid() = Arb.fromGen genInvalidPassword
