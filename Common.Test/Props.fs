module AdventOfCode20.Props

open FsCheck

let (=?) a b = a = b |@ $"%A{a} and %A{b} are equal."

let (<>?) a b = a <> b |@ $"%A{a} and %A{b} are unequal."

let rejectWith reason = false |@ reason