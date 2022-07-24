module AdventOfCode20.SeqProps

open FsCheck

let isEmpty seq = seq |> Seq.isEmpty |@ "The sequence is empty"

let isNotEmpty seq = (not <| Seq.isEmpty seq) |@ "The sequence is not empty"
