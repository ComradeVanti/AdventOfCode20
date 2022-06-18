namespace PasswordPhilosophy1

open System.Text.RegularExpressions
open FsCheck.Xunit
open PasswordPhilosophy1.PasswordGen

[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module PasswordLogTests =
    
    let entryRegex = Regex(@"\d+-\d+ [a-z]: [a-z]+")
    
    [<Property>]
    let ``Stringified logs are in the correct format`` (log: PasswordLog) =
        entryRegex.IsMatch (log.ToString())