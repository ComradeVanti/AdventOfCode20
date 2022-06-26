module AdventOfCode20.ReportRepair.MockPuzzleInputGen

open FsCheck
open AdventOfCode20

let genExpense: Gen<Expense> = Gen.choose (100, 2000)

let genCorePair =
    gen {
        let! lower = genExpense
        let upper = 2020 - lower
        return [ lower; upper ]
    }

let genValidExpenseFor list =
    genExpense
    |> Gen.filter (fun expense ->
        not (list |> List.exists (fun other -> other + expense = 2020)))

let genLargerList list =
    gen {
        let! expense = genValidExpenseFor list
        let! index = Gen.indexIn list
        return list |> List.insertAt index expense
    }

let genInput =
    Gen.sized (fun size ->
        gen {
            let! pair = genCorePair
            let pairProduct = pair |> List.mult
            let! list = Gen.repeat pair size genLargerList

            return { Report = Entries list; PairProduct = pairProduct }
        })

type ArbMockPuzzleInput =
    static member Default() = Arb.fromGen genInput
