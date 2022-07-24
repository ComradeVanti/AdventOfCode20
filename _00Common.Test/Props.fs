module AdventOfCode20.Props

open FsCheck

let (=?) a b = a = b |@ $"%A{a} and %A{b} are equal."

let (<>?) a b = a <> b |@ $"%A{a} and %A{b} are unequal."

let rejectWith reason = false |@ reason

let generatesVariety size sampleSize g =
    (g |> Gen.sample size sampleSize |> (not << List.allEqual))
    |@ "Generator generates variety"

let usesSize g =
    let seed = Random.newSeed ()

    List.init 10 id
    |> List.map (fun size -> g |> Gen.eval size seed)
    |> (not << List.allEqual)
    |@ "Generator uses size"

let usesSizeFor selector g =
    let seed = Random.newSeed ()

    List.init 10 id
    |> List.map (fun size -> g |> Gen.eval size seed)
    |> List.map selector
    |> (not << List.allEqual)
    |@ "Property depends on size"
