module PasswordPhilosophy1.PuzzleInputGen

open FsCheck
open PasswordGen

type PuzzleInput = { Entries: PasswordLog list; ValidCount: int }

let genInput =
    Gen.sized (fun size ->
        gen {
            let! validCount = Gen.choose (0, size)
            let! invalidCount = Gen.choose (0, size)

            let! valid = Gen.listOfLength validCount genValidLog
            let! invalid = Gen.listOfLength invalidCount genInvalidLog

            return { Entries = valid @ invalid; ValidCount = validCount }
        })

type ArbPuzzleInput =
    static member Default() = Arb.fromGen genInput
