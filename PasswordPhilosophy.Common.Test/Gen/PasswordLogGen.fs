module AdventOfCode20.PasswordPhilosophy.PasswordLogGen

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open AdventOfCode20.CharGen
open FsCheck
open Microsoft.FSharp.Collections

type ValidPassword = ValidPassword of PasswordLog
type InvalidPassword = InvalidPassword of PasswordLog


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

let private genInvalidPasswordFor policy =
    gen {
        let minLength = policy.MaxCount + 1
        let maxLength = minLength + 10

        let forbiddenIndices =
            Set.ofList [ policy.MinCount - 1; policy.MaxCount - 1 ]

        let! length = Gen.choose (minLength, maxLength)

        let! policyLetterCount =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        if length > policy.MaxCount + 2 then
                            Gen.choose (policy.MaxCount + 1, length - 2)]

        let genIndex = Gen.choose (0, length - 1)
        let genPolicyLetter = Gen.constant policy.Letter
        let genNonPolicyLetter = genLetter |> Gen.except policy.Letter

        let! policyLetterIndices =
            genIndex
            |> Gen.exceptAllIn forbiddenIndices
            |> Gen.setWithCount policyLetterCount

        return!
            Gen.initList length (fun index ->
                if policyLetterIndices |> Set.contains index then
                    genPolicyLetter
                else
                    genNonPolicyLetter)
            |> Gen.map System.String.Concat
    }

let genValidPasswordLog =
    gen {
        let! policy = genPolicy
        let! password = genValidPasswordFor policy
        return { Password = password; Policy = policy }
    }

let genInvalidPasswordLog =
    gen {
        let! policy = genPolicy
        let! password = genInvalidPasswordFor policy
        return { Password = password; Policy = policy }
    }

let genPasswordLog =
    Gen.oneof [ genValidPasswordLog; genInvalidPasswordLog ]

type ArbPasswordLogs =
    static member Mixed() = Arb.fromGen genPasswordLog

    static member Valid() =
        Arb.fromGen (genValidPasswordLog |> Gen.map ValidPassword)

    static member Invalid() =
        Arb.fromGen (genInvalidPasswordLog |> Gen.map InvalidPassword)
