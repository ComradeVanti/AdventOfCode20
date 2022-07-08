module AdventOfCode20.PasswordPhilosophy.PolicyGen

open AdventOfCode20
open FsCheck
open AdventOfCode20.CharGen

let private genMinCount = Gen.choose (1, 5)

let private genMaxCountFor minCount = Gen.choose (minCount + 1, minCount + 5)

let genPolicy =
    gen {
        let! letter = genLetter
        let! minCount = genMinCount
        let! maxCount = genMaxCountFor minCount

        return
            { Letter = letter
              MinCount = minCount
              MaxCount = maxCount }
    }

type ArbPolicies =
    static member Default() = Arb.fromGen genPolicy
