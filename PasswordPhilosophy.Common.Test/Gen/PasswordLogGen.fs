module AdventOfCode20.PasswordPhilosophy.PasswordLogGen

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open AdventOfCode20.CharGen
open FsCheck

type ValidPassword = ValidPassword of PasswordLog
type InvalidPassword = InvalidPassword of PasswordLog

let private genLargerPassword password times forbiddenLetter =
    let genAllowedLetter = genLetter |> Gen.except forbiddenLetter

    let genLarger original =
        gen {
            let! letter = genAllowedLetter
            let! index = original |> Seq.toList |> Gen.indexIn
            return original |> String.applySeqF (Seq.insertAt index letter)
        }

    Gen.repeat password times genLarger


let private genPasswordWith letter count =
    gen {
        let minGrowCount = if count = 0 then 1 else 0
        let! growCount = Gen.choose (minGrowCount, 10)
        let basePassword = String.repeatChar count letter
        return! genLargerPassword basePassword growCount letter
    }


let private genValidPasswordFor policy =
    gen {
        let! count = Gen.choose (policy.MinCount, policy.MaxCount)
        return! genPasswordWith policy.Letter count
    }

let private genInvalidPasswordFor policy =
    gen {
        let! count =
            Gen.oneof [ Gen.choose (0, policy.MinCount - 1)
                        Gen.choose (policy.MaxCount + 1, policy.MaxCount + 10) ]

        return! genPasswordWith policy.Letter count
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
