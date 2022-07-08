namespace AdventOfCode20.PasswordPhilosophy

open AdventOfCode20
open AdventOfCode20.Props
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen
open AdventOfCode20.PasswordPhilosophy.PolicyGenTests
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module PasswordLogGenTests =

    let matchesLetterCount log =
        let letter = log.Policy.Letter
        let count = log.Password |> Seq.countItem letter

        let minCountIsValid =
            count >= log.Policy.MinCount
            |@ $"Min-count valid (%i{count})"

        let maxCountIsValid =
            count <= log.Policy.MaxCount
            |@ $"Max-count valid (%i{count})"

        (minCountIsValid .&. maxCountIsValid) |@ "Matches count"

    let doesNotMatchLetterCount log =
        let letter = log.Policy.Letter
        let count = log.Password |> Seq.countItem letter

        let minCountIsInvalid =
            count < log.Policy.MinCount
            |@ $"Min-count invalid (%i{count})"

        let maxCountIsInvalid =
            count > log.Policy.MaxCount
            |@ $"Max-count invalid (%i{count})"

        (minCountIsInvalid .|. maxCountIsInvalid)
        |@ "Does not match count"

    let matchesLetterPositions log =
        let posA = log.Policy.MinCount - 1
        let posB = log.Policy.MaxCount - 1

        let letterA =
            log.Password
            |> String.tryCharAt posA
            |> Option.defaultValue '?'

        let letterB =
            log.Password
            |> String.tryCharAt posB
            |> Option.defaultValue '?'

        let positionAIsValid =
            letterA =? log.Policy.Letter
            |@ $"Position A correct (%i{posA})"

        let positionBIsValid =
            letterB =? log.Policy.Letter
            |@ $"Position B correct (%i{posB})"

        (positionAIsValid .&. positionBIsValid)
        |@ "Matches positions"

    let doesNotMatchLetterPositions log =
        let posA = log.Policy.MinCount - 1
        let posB = log.Policy.MaxCount - 1

        let letterA =
            log.Password
            |> String.tryCharAt posA
            |> Option.defaultValue '?'

        let letterB =
            log.Password
            |> String.tryCharAt posB
            |> Option.defaultValue '?'

        let positionAIsValid =
            letterA <>? log.Policy.Letter
            |@ $"Position A incorrect (%i{posA})"

        let positionBIsValid =
            letterB <>? log.Policy.Letter
            |@ $"Position B incorrect (%i{posB})"

        (positionAIsValid .&. positionBIsValid)
        |@ "Does not match positions"

    let matchesDay1 log = log |> matchesLetterCount |@ "Matches day 1"

    let matchesDay2 log = log |> matchesLetterPositions |@ "Matches day 2"

    let doesNotMatchDay1 log =
        log |> doesNotMatchLetterCount |@ "Does not match day 1"

    let doesNotMatchDay2 log =
        log |> doesNotMatchLetterPositions |@ "Does not match day 2"

    [<Property>]
    let ``Policy is valid`` log = log.Policy |> policyIsValid

    [<Property>]
    let ``Password has valid length`` log =
        let lenght = log.Password |> String.length

        let hasMinimumLength =
            lenght >= MinPasswordLength
            |@ $"Password has minimum length (%i{lenght})"

        hasMinimumLength

    [<Property>]
    let ``MatchingDay1 generated correctly`` (MatchingDay1 log) =
        (log |> matchesDay1) .&. (log |> doesNotMatchDay2)
        |@ "Matches only day 1"

    [<Property>]
    let ``MatchingDay2 generated correctly`` (MatchingDay2 log) =
        log |> matchesDay2 |@ "Matches only day 2"

    [<Property>]
    let ``MatchingBoth generated correctly`` (MatchingBoth log) =
        (log |> matchesDay1) .&. (log |> matchesDay2)
        |@ "Matches both days"

    [<Property>]
    let ``MatchingNeither generated correctly`` (MatchingNeither log) =
        (log |> doesNotMatchDay1) .&. (log |> doesNotMatchDay2)
        |@ "Matches neither days"

    let logIsValid log =
        log |> ``Policy is valid``
        .&. (log |> ``Password has valid length``)
