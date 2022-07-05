namespace AdventOfCode20.PasswordPhilosophy

open AdventOfCode20
open AdventOfCode20.PasswordPhilosophy.PasswordLogGen
open FsCheck
open FsCheck.Xunit


[<Properties(Arbitrary = [| typeof<ArbPasswordLogs> |])>]
module ParseTests =

    let stringify log =
        $"%i{log.Policy.MinCount}-%i{log.Policy.MaxCount} %c{log.Policy.Letter}: %s{log.Password}"


    [<Property>]
    let ``Entries are parsed correctly`` log =
        let s = log |> stringify

        match Parse.log s with
        | Some parsed -> (parsed = log) |@ "Parsed correctly"
        | None -> false |@ "Could parse"
