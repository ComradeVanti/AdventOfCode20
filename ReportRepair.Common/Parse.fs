[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.ReportRepair.Parse

open AdventOfCode20

let private entry s : Expense option = Parse.int s

let expenseReport lines =
    lines
    |> List.map entry
    |> Option.collect
    |> Option.map Entries
