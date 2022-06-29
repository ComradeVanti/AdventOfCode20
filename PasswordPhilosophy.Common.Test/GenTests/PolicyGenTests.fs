namespace AdventOfCode20.PasswordPhilosophy

open System
open AdventOfCode20.PasswordPhilosophy.PolicyGen
open FsCheck
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbPolicies> |])>]
module PolicyGenTests =

    [<Property>]
    let ``Letters are valid`` policy =

        let isLetter =
            policy.Letter |> Char.IsLetter
            |@ $"%A{policy.Letter} is a letter"

        let isLowercase =
            policy.Letter |> Char.IsLower
            |@ $"%A{policy.Letter} is lowercase"

        isLetter .&. isLowercase
        
    [<Property>]
    let ``Min-counts are valid`` policy =
        
        let minimumIsValid =
            let validMinimum = 1
            policy.MinCount >= validMinimum
            |@ $"Count (%d{policy.MinCount}) is >= 1"
            
        let maximumIsValid =
            let validMaximum = 5
            policy.MinCount <= validMaximum
            |@ $"Count (%d{policy.MinCount}) is <= 5"
            
        minimumIsValid .&. maximumIsValid

    [<Property>]
    let ``Max-counts are valid`` policy =
        
        let minimumIsValid =
            let validMinimum = policy.MinCount + 1
            policy.MaxCount >= validMinimum
            |@ $"Count (%d{policy.MaxCount}) is >= %d{validMinimum}"
            
        let maximumIsValid =
            let validMaximum = policy.MinCount + 5
            policy.MaxCount <= validMaximum
            |@ $"Count (%d{policy.MaxCount}) is <= %d{validMaximum}"
            
        minimumIsValid .&. maximumIsValid