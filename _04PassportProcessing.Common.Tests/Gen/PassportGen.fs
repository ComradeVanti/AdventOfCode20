module AdventOfCode20.PassportProcessing.PassportGen

open AdventOfCode20
open AdventOfCode20.CharGen
open FsCheck

let private genId = Gen.stringFrom genDigit 9

let private genBirthYear =
    Gen.choose (1900, 2000) |> Gen.map string

let private genIssueYear =
    Gen.choose (1900, 2000) |> Gen.map string

let private genExpirationYear =
    Gen.choose (1900, 2000) |> Gen.map string

let private genCmHeight =
    gen {
        let! value = Gen.choose (150, 200)
        return $"%i{value}cm"
    }

let private genInchHeight =
    gen {
        let! value = Gen.choose (58, 60)
        return $"%i{value}in"
    }

let private genHeight = Gen.oneof [ genCmHeight; genInchHeight ]

let private genHairColor = Gen.stringFrom genHexDigit 6

let private genEyeColor =
    Gen.elements [ "hzl"
                   "oth"
                   "lzr"
                   "blu"
                   "gry"
                   "amb"
                   "brn"
                   "grn" ]

let private genCountryId =
    Gen.choose (10, 200) |> Gen.map string

let private forSure g = g |> Gen.map Some

let private maybe g = Gen.oneof [ forSure g; Gen.constant None ]

let genValidPassport =
    gen {
        let! id = forSure genId
        let! birthYear = forSure genBirthYear
        let! issueYear = forSure genIssueYear
        let! expirationYear = forSure genExpirationYear
        let! height = forSure genHeight
        let! hairColor = forSure genHairColor
        let! eyeColor = forSure genEyeColor
        let! countryId = maybe genCountryId

        return
            { Id = id
              BirthYear = birthYear
              IssueYear = issueYear
              ExpirationYear = expirationYear
              Height = height
              HairColor = hairColor
              EyeColor = eyeColor
              CountryId = countryId }
    }

let genInvalidPassport =
    Gen.constant
        { Id = None
          BirthYear = None
          IssueYear = None
          ExpirationYear = None
          Height = None
          HairColor = None
          EyeColor = None
          CountryId = None }

let genPassport =
    Gen.oneof [ genValidPassport; genInvalidPassport ]


type ValidPassport = ValidPassport of Passport

type InvalidPassport = InvalidPassport of Passport

type ArbPassports =

    static member Valid() =
        Arb.fromGen (genValidPassport |> Gen.map ValidPassport)

    static member Invalid() =
        Arb.fromGen (genInvalidPassport |> Gen.map InvalidPassport)

    static member Mixed() = Arb.fromGen genPassport
