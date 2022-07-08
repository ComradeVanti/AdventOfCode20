module AdventOfCode20.CharGen

open FsCheck

let genLetter = Gen.choose (int 'a', int 'z') |> Gen.map char
