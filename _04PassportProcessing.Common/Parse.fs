[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.PassportProcessing.Parse

open AdventOfCode20

let private getEntries lines = lines |> List.collect (String.splitAt ' ')

let private getEntryValue entry = entry |> String.splitAt ':' |> List.last

let private isEntryOfType typeName entry =
    entry |> String.splitAt ':' |> List.head = typeName

let private parsePassport lines =
    let entries = getEntries lines

    let entryFor typeName =
        entries
        |> List.tryFind (isEntryOfType typeName)
        |> Option.map getEntryValue

    { Id = entryFor "pid"
      CountryId = entryFor "cid"
      BirthYear = entryFor "byr"
      IssueYear = entryFor "iyr"
      ExpirationYear = entryFor "eyr"
      Height = entryFor "hgt"
      HairColor = entryFor "hcl"
      EyeColor = entryFor "ecl" }


let private splitIntoPassportLines lines = lines |> List.splitAtItem ""

let batch lines =
    lines
    |> splitIntoPassportLines
    |> List.map parsePassport
    |> Some
    |> Option.map Passports
