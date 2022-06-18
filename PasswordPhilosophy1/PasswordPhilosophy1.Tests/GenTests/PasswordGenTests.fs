namespace PasswordPhilosophy1

open FsCheck
open FsCheck.Xunit
open PasswordPhilosophy1.PasswordGen

[<Properties(Arbitrary = [| typeof<ArbPasswords> |])>]
module PasswordGenTests =

    let countChar c s = s |> Seq.filter ((=) c) |> Seq.length

    let countCharsExcept c s = s |> Seq.filter ((<>) c) |> Seq.length

    [<Property>]
    let ``Valid passwords contain the policy letter the correct number of times``
        (ValidPasswordWithPolicy (password, policy))
        =
        let count = password |> countChar policy.Character

        let overMinCount = count >= policy.MinCount |@ "Over min count"

        let underMaxCount =
            count <= policy.MaxCount |@ "Under max count"

        overMinCount .&. underMaxCount

    [<Property>]
    let ``Valid passwords contain between 1 and 10 non-policy letters``
        (ValidPasswordWithPolicy (password, policy))
        =
        let otherCounts = password |> countCharsExcept policy.Character

        let overMinCount = otherCounts >= 1 |@ "Over min count"
        let underMaxCount = otherCounts <= 10 |@ "Under max count"

        overMinCount .&. underMaxCount

    [<Property>]
    let ``Invalid passwords contain the policy letter not often enough or too often``
        (InvalidPasswordWithPolicy (password, policy))
        =
        let count = password |> countChar policy.Character

        let underMinCount =
            count < policy.MinCount
            |@ $"Under min count (%d{count})"

        let overMaxCount =
            count > policy.MaxCount
            |@ $"Over max count (%d{count})"

        underMinCount .|. overMaxCount

    [<Property>]
    let ``Invalid passwords contain between 1 and 10 non-policy letters``
        (InvalidPasswordWithPolicy (password, policy))
        =
        let otherCounts = password |> countCharsExcept policy.Character

        let overMinCount =
            otherCounts >= 1 |@ $"Over min count %d{otherCounts}"

        let underMaxCount =
            otherCounts <= 10 |@ $"Under max count %d{otherCounts}"

        overMinCount .&. underMaxCount
