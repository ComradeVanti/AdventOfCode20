module AdventOfCode20.Props

open FsCheck

let (=?) a b = a = b |@ $"Expected %A{b} but got %A{a}."

let rejectWith reason = false |@ reason
