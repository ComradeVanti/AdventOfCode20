namespace AdventOfCode20.PassportProcessing

type Passport =
    { Id: string option
      CountryId: string option
      BirthYear: string option
      IssueYear: string option
      ExpirationYear: string option
      Height: string option
      HairColor: string option
      EyeColor: string option }

type Batch = Passports of Passport list
