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

    let matchesStar1 = matchesLetterCount

    let matchesStar2 = matchesLetterPositions

    let doesNotMatchStar1 = not << matchesStar1

    let doesNotMatchStar2 = not << matchesStar2

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
    let ``MatchingStar1 generated correctly`` (MatchingStar1 log) =
        (log |> matchesStar1) && (log |> doesNotMatchStar2)

    [<Property>]
    let ``MatchingStar2 generated correctly`` (MatchingStar2 log) =
        (log |> doesNotMatchStar1) && (log |> matchesStar2)

    [<Property>]
    let ``MatchingBoth generated correctly`` (MatchingBoth log) =
        (log |> matchesStar1) && (log |> matchesStar2)

    [<Property>]
    let ``MatchingNeither generated correctly`` (MatchingNeither log) =
        (log |> doesNotMatchStar1) && (log |> doesNotMatchStar2)

    let logIsValid log =
        log |> ``Policy is valid``
        .&. (log |> ``Password has valid length``)
