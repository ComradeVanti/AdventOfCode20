namespace PasswordPhilosophy1

open FsCheck
open FsCheck.Xunit
open PasswordPhilosophy1.PasswordGen

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module PasswordLogGenTests =

    let countChar c s = s |> Seq.filter ((=) c) |> Seq.length

    let countCharsExcept c s = s |> Seq.filter ((<>) c) |> Seq.length

    [<Property>]
    let ``Valid passwords contain the policy letter the correct number of times``
        (ValidLog log)
        =
        let count = log.Password |> countChar log.Policy.Character

        let overMinCount =
            count >= log.Policy.MinCount |@ "Over min count"

        let underMaxCount =
            count <= log.Policy.MaxCount |@ "Under max count"

        overMinCount .&. underMaxCount

    [<Property>]
    let ``Valid passwords contain between 1 and 10 non-policy letters``
        (ValidLog log)
        =
        let otherCounts =
            log.Password |> countCharsExcept log.Policy.Character

        let overMinCount = otherCounts >= 1 |@ "Over min count"
        let underMaxCount = otherCounts <= 10 |@ "Under max count"

        overMinCount .&. underMaxCount

    [<Property>]
    let ``Invalid passwords contain the policy letter not often enough or too often``
        (InvalidLog log)
        =
        let count = log.Password |> countChar log.Policy.Character

        let underMinCount =
            count < log.Policy.MinCount
            |@ $"Under min count (%d{count})"

        let overMaxCount =
            count > log.Policy.MaxCount |@ $"Over max count (%d{count})"

        underMinCount .|. overMaxCount

    [<Property>]
    let ``Invalid passwords contain between 1 and 10 non-policy letters``
        (InvalidLog log)
        =
        let otherCounts =
            log.Password |> countCharsExcept log.Policy.Character

        let overMinCount =
            otherCounts >= 1 |@ $"Over min count %d{otherCounts}"

        let underMaxCount =
            otherCounts <= 10 |@ $"Under max count %d{otherCounts}"

        overMinCount .&. underMaxCount
