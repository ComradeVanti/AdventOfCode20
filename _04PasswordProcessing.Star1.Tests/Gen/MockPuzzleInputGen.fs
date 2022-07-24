module AdventOfCode20.PasswordProcessing.Star1.MockPuzzleInputGen

open AdventOfCode20
open AdventOfCode20.PasswordProcessing
open AdventOfCode20.PasswordProcessing.PassportGen
open FsCheck

let private genBatchOfLength length validCount =
    let invalidCount = length - validCount

    gen {
        let! valid = genValidPassport |> Gen.listOfLength validCount
        let! invalid = genInvalidPassport |> Gen.listOfLength invalidCount
        let! passports = (valid @ invalid) |> Gen.shuffledList
        return Passports passports
    }

let private genInputOfLength length =
    gen {
        let! validCount = Gen.choose (0, length / 2)
        let! batch = genBatchOfLength length validCount
        return { Batch = batch; ValidCount = validCount }
    }

let genInput =
    Gen.sized (fun size ->
        let length = min size 100
        genInputOfLength length)

type ArbPuzzleInput =
    static member Default() = Arb.fromGen genInput
