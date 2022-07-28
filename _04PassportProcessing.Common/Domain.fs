namespace AdventOfCode20.PassportProcessing

type Year = int

type HeightUnit =
    | Centimeter
    | Inch

type Height = int * HeightUnit

type HexColor = string

type Passport =
    { Id: string option
      CountryId: int option
      BirthYear: Year option
      IssueYear: Year option
      ExpirationYear: Year option
      Height: Height option
      HairColor: string option
      EyeColor: string option }

type Batch = Passports of Passport list
