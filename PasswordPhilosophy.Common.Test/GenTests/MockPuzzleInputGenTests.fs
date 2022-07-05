namespace AdventOfCode20.PasswordPhilosophy

open AdventOfCode20
open AdventOfCode20.SeqProps
open AdventOfCode20.PasswordPhilosophy.MockPuzzleInputGen
open AdventOfCode20.PasswordPhilosophy.PasswordLogGenTests
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbInput> |])>]
module MockPuzzleInputGenTests =

    let private logsIn input =
        let (Logs logs) = input.Report
        logs

    [<Property>]
    let ``Reports are not empty`` input = (logsIn input) |> isNotEmpty

    [<Property>]
    let ``All logs are valid`` input =
        (logsIn input) |> List.map logIsValid |> Prop.all
