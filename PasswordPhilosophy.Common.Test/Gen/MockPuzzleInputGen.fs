module AdventOfCode20.PasswordPhilosophy.MockPuzzleInputGen

open FsCheck
open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen

let private genInputWithCount count =
    gen {
        let! star1Count, star2Count, neitherCount, bothCount =
            (Gen.split4 0 count)

        let! star1 = Gen.listOfLength star1Count genMatchingStar1Log
        let! star2 = Gen.listOfLength star2Count genMatchingStar2Log
        let! neither = Gen.listOfLength neitherCount genMatchingNeitherLog
        let! both = Gen.listOfLength bothCount genMatchingBothLog

        let matchingStar1Count = star1Count + bothCount
        let matchingStar2Count = star2Count + bothCount

        let! logs =
            List.concat [ star1; star2; neither; both ]
            |> Gen.shuffledList

        return
            { Report = Logs logs
              MatchingStar1Count = matchingStar1Count
              MatchingStar2Count = matchingStar2Count }
    }

let private genInput =
    Gen.sized (fun s ->
        gen {
            let! count = Gen.choose (1, s)
            return! genInputWithCount count
        })

type ArbMockPuzzleInput =
    static member Default() = Arb.fromGen genInput
