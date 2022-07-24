namespace AdventOfCode20.PasswordProcessing

open AdventOfCode20
open AdventOfCode20.PasswordProcessing.PassportGen
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPassports> |])>]
module PassportGenTests =

    let private hasField fieldValue fieldName =
        fieldValue |> Option.isSome |@ $"Has %s{fieldName}"

    let private fieldIsMissing fieldValue fieldName =
        fieldValue |> Option.isNone
        |@ $"Field \"%s{fieldName}\" is missing"

    [<Property>]
    let ``Valid passports are missing no required field``
        (ValidPassport passport)
        =
        (hasField passport.Id "id")
        .&. (hasField passport.BirthYear "birth-year")
        .&. (hasField passport.IssueYear "issue-year")
        .&. (hasField passport.ExpirationYear "expiration-year")
        .&. (hasField passport.Height "height")
        .&. (hasField passport.HairColor "hair-color")
        .&. (hasField passport.EyeColor "eye-year")

    [<Property>]
    let ``Invalid passports are missing one ore more required fields``
        (InvalidPassport passport)
        =
        (fieldIsMissing passport.Id "id")
        .|. (fieldIsMissing passport.BirthYear "birth-year")
        .|. (fieldIsMissing passport.IssueYear "issue-year")
        .|. (fieldIsMissing passport.ExpirationYear "expiration-year")
        .|. (fieldIsMissing passport.Height "height")
        .|. (fieldIsMissing passport.HairColor "hair-color")
        .|. (fieldIsMissing passport.EyeColor "eye-year")
