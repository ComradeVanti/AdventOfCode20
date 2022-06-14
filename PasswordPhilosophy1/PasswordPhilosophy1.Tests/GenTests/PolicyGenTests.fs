namespace PasswordPhilosophy1

open System
open FsCheck
open FsCheck.Xunit
open PasswordPhilosophy1.PolicyGen

[<Properties(Arbitrary = [| typeof<ArbPolicy> |])>]
module PolicyGenTests =

    [<Property>]
    let ``Letters are valid`` (policy: Policy) =
        let isLetter =
            (policy.Character |> Char.IsLetter) |@ "Is letter"

        let isLowercase =
            (policy.Character |> Char.IsLower) |@ "Is lowercase"

        isLetter .&. isLowercase

    [<Property>]
    let ``Counts are in valid`` (policy: Policy) =
        let maxLargerThanMin =
            (policy.MaxCount > policy.MinCount) |@ "Max larger than min"

        let minInValidRange =
            (policy.MinCount >= Policy.MinMinCount
             && policy.MinCount <= Policy.MaxMinCount)
            |@ "Min in valid range"

        let rangeValid =
            let range = policy.MaxCount - policy.MinCount

            (range >= Policy.MinRange && range <= Policy.MaxRange)
            |@ "Range is valid"

        maxLargerThanMin .&. minInValidRange .&. rangeValid
