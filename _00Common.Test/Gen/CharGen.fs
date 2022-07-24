module AdventOfCode20.CharGen

open FsCheck

let genCharBetween (min: char) (max: char) =
    Gen.choose (int min, int max) |> Gen.map char

let genLetter = genCharBetween 'a' 'z'

let genDigit = genCharBetween '0' '9'

let genHexDigit = Gen.oneof [ genDigit; genCharBetween 'a' 'f' ]
