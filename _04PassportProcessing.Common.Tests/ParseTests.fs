module AdventOfCode20.PassportProcessing.ParseTests

open AdventOfCode20
open Xunit

[<Fact>]
let ``Can parse example-input`` () =
    let batch = ExampleInput.batch

    Assert.True(batch |> Option.isSome, "Could not parse")

    let (Passports passports) = batch |> Option.get
    let passportCount = passports |> List.length
    Assert.Equal(4, passportCount)
