namespace AdventOfCode20.PasswordPhilosophy

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen
open AdventOfCode20.PasswordPhilosophy.PolicyGenTests
open FsCheck
open FsCheck.Xunit

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module PasswordLogGenTests =

    let matchesLetterCount log =
        let letter = log.Policy.Letter
        let count = log.Password |> Seq.countItem letter

        let minCountIsValid = count >= log.Policy.MinCount
        let maxCountIsValid = count <= log.Policy.MaxCount

        minCountIsValid && maxCountIsValid

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

        let positionAIsValid = letterA = log.Policy.Letter
        let positionBIsValid = letterB = log.Policy.Letter

        positionAIsValid ||! positionBIsValid

    let matchesDay1 = matchesLetterCount

    let matchesDay2 = matchesLetterPositions

    let doesNotMatchDay1 = not << matchesDay1

    let doesNotMatchDay2 = not << matchesDay2

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
        (log |> matchesDay1) && (log |> doesNotMatchDay2)

    [<Property>]
    let ``MatchingDay2 generated correctly`` (MatchingDay2 log) =
        (log |> doesNotMatchDay1) && (log |> matchesDay2)

    [<Property>]
    let ``MatchingBoth generated correctly`` (MatchingBoth log) =
        (log |> matchesDay1) && (log |> matchesDay2)

    [<Property>]
    let ``MatchingNeither generated correctly`` (MatchingNeither log) =
        (log |> doesNotMatchDay1) && (log |> doesNotMatchDay2)

    let logIsValid log =
        log |> ``Policy is valid``
        .&. (log |> ``Password has valid length``)
