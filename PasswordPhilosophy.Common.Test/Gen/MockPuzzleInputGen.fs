module AdventOfCode20.PasswordPhilosophy.MockPuzzleInputGen

open FsCheck
open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen

let private genInputWithCount count =
    gen {
        let! day1Count, day2Count, neitherCount, bothCount =
            (Gen.split4 0 count)

        let! day1 = Gen.listOfLength day1Count genMatchingDay1Log
        let! day2 = Gen.listOfLength day2Count genMatchingDay2Log
        let! neither = Gen.listOfLength neitherCount genMatchingNeitherLog
        let! both = Gen.listOfLength bothCount genMatchingBothLog

        let matchingDay1Count = day1Count + bothCount
        let matchingDay2Count = day2Count + bothCount

        let! logs =
            List.concat [ day1; day2; neither; both ]
            |> Gen.shuffledList

        return
            { Report = Logs logs
              MatchingDay1Count = matchingDay1Count
              MatchingDay2Count = matchingDay2Count }
    }

let private genInput =
    Gen.sized (fun s ->
        gen {
            let! count = Gen.choose (1, s)
            return! genInputWithCount count
        })

type ArbMockPuzzleInput =
    static member Default() = Arb.fromGen genInput
