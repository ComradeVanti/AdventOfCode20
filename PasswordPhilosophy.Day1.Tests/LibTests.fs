namespace AdventOfCode20.PasswordPhilosophy.Day1

open AdventOfCode20.Props
open AdventOfCode20.PasswordPhilosophy
open AdventOfCode20.PasswordPhilosophy.MockPuzzleInputGen
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbMockPuzzleInput> |])>]
module LibTests =

    [<Property>]
    let ``Finds correct count`` input =
        countValid input.Report =? input.ValidCount
