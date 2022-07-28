[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.PassportProcessing.Star1.Lib

open AdventOfCode20
open AdventOfCode20.PassportProcessing

let private hasField = Option.isSome

let private isValid passport =
    hasField passport.Id
    && hasField passport.BirthYear
    && hasField passport.IssueYear
    && hasField passport.ExpirationYear
    && hasField passport.Height
    && hasField passport.HairColor
    && hasField passport.EyeColor

let countValid batch =
    let (Passports passports) = batch

    passports |> List.countWith isValid
