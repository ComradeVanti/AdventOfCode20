module AdventOfCode20.PassportProcessing.ParseTests

open System.Reflection
open AdventOfCode20
open Xunit

[<Fact>]
let ``Can parse example-input`` () =
    let lines = ExampleInput.lines (Assembly.GetExecutingAssembly())
    let parsed = Parse.batch lines
    Assert.True(parsed |> Option.isSome)
