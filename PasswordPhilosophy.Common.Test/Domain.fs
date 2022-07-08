[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.PasswordPhilosophy.Domain

type MockPuzzleInput = {
    Report: PasswordReport
    MatchingDay1Count: int
    MatchingDay2Count: int
}

[<Literal>]
let MinPasswordLength = 4