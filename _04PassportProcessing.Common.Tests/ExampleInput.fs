[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.PassportProcessing.ExampleInput

open System.Reflection
open AdventOfCode20

let batch =
    let lines = ExampleInput.lines (Assembly.GetExecutingAssembly())
    Parse.batch lines
