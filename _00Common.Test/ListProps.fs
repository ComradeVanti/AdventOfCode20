module AdventOfCode20.ListProps

open FsCheck

let hasAtLeastLengthOf min list =
    let length = list |> List.length
    length >= min |@ $"List only had a length of %d{length}"
