module PasswordPhilosophy1.PolicyGen

open FsCheck

let genLetter = Gen.choose (97, 122) |> Gen.map char

let genMinCount =
    Gen.choose (Policy.MinMinCount, Policy.MaxMinCount)

let genMaxCount minCount =
    Gen.choose (Policy.MinRange, Policy.MaxRange)
    |> Gen.map (fun offset -> minCount + offset)

let genPolicy =
    gen {
        let! letter = genLetter
        let! minCount = genMinCount
        let! maxCount = genMaxCount minCount
        return Policy(letter, minCount, maxCount)
    }

type ArbPolicy =
    static member Default() = Arb.fromGen genPolicy
