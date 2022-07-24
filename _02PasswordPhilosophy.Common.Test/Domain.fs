[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.PasswordPhilosophy.Domain

type MockPuzzleInput =
    { Report: PasswordReport
      MatchingStar1Count: int
      MatchingStar2Count: int }

[<Literal>]
let MinPasswordLength = 4
