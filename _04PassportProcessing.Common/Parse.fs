[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.PassportProcessing.Parse

open System
open AdventOfCode20

let private getEntries lines = lines |> List.collect (String.splitAt ' ')

let private getEntryValue entry = entry |> String.splitAt ':' |> List.last

let private isEntryOfType typeName entry =
    entry |> String.splitAt ':' |> List.head = typeName

let private tryParseId s = Some s

let private tryParseCountryId = Parse.int

let private tryParseYear = Parse.int

let private tryParseHeight s =
    opt {
        let unit = s |> String.applySeqF (Seq.filter Char.IsLetter)

        let! value =
            s |> String.applySeqF (Seq.filter Char.IsDigit) |> Parse.int

        if unit = "cm" then
            return (value, Centimeter)
        else
            return (value, Inch)
    }

let private parsePassport lines =
    let entries = getEntries lines

    let entryOfType typeName =
        entries
        |> List.tryFind (isEntryOfType typeName)
        |> Option.map getEntryValue

    let tryParseWith parser entry =
        match entry with
        | Some value -> parser value |> Option.map Some
        | None -> Some None

    opt {
        let! id = entryOfType "pid" |> tryParseWith tryParseId
        let! countryId = entryOfType "cid" |> tryParseWith tryParseCountryId
        let! birthYear = entryOfType "byr" |> tryParseWith tryParseYear
        let! issueYear = entryOfType "iyr" |> tryParseWith tryParseYear
        let! expirationYear = entryOfType "eyr" |> tryParseWith tryParseYear
        let! height = entryOfType "hgt" |> tryParseWith tryParseHeight
        let hairColor = entryOfType "hcl"
        let eyeColor = entryOfType "ecl"

        return
            { Id = id
              CountryId = countryId
              BirthYear = birthYear
              IssueYear = issueYear
              ExpirationYear = expirationYear
              Height = height
              HairColor = hairColor
              EyeColor = eyeColor }
    }

let private splitIntoPassportLines lines = lines |> List.splitAtItem ""

let batch lines =
    lines
    |> splitIntoPassportLines
    |> List.map parsePassport
    |> Option.collect
    |> Option.map Passports
