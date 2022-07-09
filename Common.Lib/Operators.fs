[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.Operators

let (||!) a b = (a || b) && not (a && b)
