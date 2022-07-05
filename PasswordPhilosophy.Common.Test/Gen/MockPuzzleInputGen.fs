module AdventOfCode20.PasswordPhilosophy.MockPuzzleInputGen

open FsCheck
open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen

let private genInput =
    gen {
        let! count = Gen.choose (1, 100)

        let! validCount = Gen.choose (0, count)
        let! validLogs = Gen.listOfLength validCount genValidPasswordLog

        let invalidCount = count - validCount
        let! invalidLogs = Gen.listOfLength invalidCount genInvalidPasswordLog

        let! logs = (validLogs @ invalidLogs) |> Gen.shuffledList
        return { Report = Logs logs; ValidCount = validCount }
    }

type ArbInput =
    static member Default() = Arb.fromGen genInput
