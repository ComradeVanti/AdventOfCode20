namespace AdventOfCode20.PasswordPhilosophy

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen
open AdventOfCode20.PasswordPhilosophy.PolicyGenTests
open AdventOfCode20.SeqProps
open FsCheck
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module PasswordLogGenTests =

    [<Property>]
    let ``Policies are valid`` log = log.Policy |> policyIsValid

    [<Property>]
    let ``Passwords are not empty`` log = log.Password |> isNotEmpty

    [<Property>]
    let ``Valid passwords contain the policy letter a correct number of times``
        (ValidLog log)
        =
        let letter = log.Policy.Letter
        let count = log.Password |> Seq.countItem letter

        let minCountIsValid =
            count >= log.Policy.MinCount
            |@ $"Min-count valid (%i{count} should be >= %i{log.Policy.MinCount})"

        let maxCountIsValid =
            count <= log.Policy.MaxCount
            |@ $"Max-count valid (%i{count} should be <= %i{log.Policy.MaxCount})"

        minCountIsValid .&. maxCountIsValid
        
    [<Property>]
    let ``Invalid passwords contain the policy letter an incorrect number of times``
        (InvalidLog log)
        =
        let letter = log.Policy.Letter
        let count = log.Password |> Seq.countItem letter

        let minCountIsInvalid =
            count < log.Policy.MinCount
            |@ $"Min-count invalid (%i{count} should be < %i{log.Policy.MinCount})"

        let maxCountIsInvalid =
            count > log.Policy.MaxCount
            |@ $"Max-count invalid (%i{count} should be > %i{log.Policy.MaxCount})"

        minCountIsInvalid .|. maxCountIsInvalid
