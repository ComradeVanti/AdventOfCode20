module AdventOfCode20.ReportRepair.MockPuzzleInputGen

open FsCheck
open AdventOfCode20

let genExpense: Gen<Expense> = Gen.choose (100, 2000)

let genTripletList =
    gen {
        let! mid = Gen.choose (500, 1500)
        let! lower = Gen.choose (100, 2020 - mid - 100)
        let upper = 2020 - (mid + lower)
        return [ lower; mid; upper ]
    }

let tryFind2020Pair triplet =
    triplet
    |> List.pairs
    |> Seq.tryFind (fun (a, b) -> a + b = 2020)
    |> Option.map (fun (a, b) -> [ a; b ])

let genPairList triplet =
    let pairs = triplet |> List.pairs |> Seq.toList

    gen {
        let! lower = genExpense
        let upper = 2020 - lower
        return [ lower; upper ]
    }
    |> Gen.filter (fun expenses ->
        not (
            expenses
            |> List.exists (fun expense ->
                pairs |> List.exists (fun (a, b) -> a + b + expense = 2020))
        ))

let genValidExpenseFor list =

    let pairs = list |> List.pairs |> Seq.toList

    let isValid expense =
        not (
            pairs
            |> List.exists (fun (a, b) ->
                a + expense = 2020
                || b + expense = 2020
                || a + b + expense = 2020)
        )

    genExpense |> Gen.filter isValid

let genLargerList list =
    gen {
        let! expense = genValidExpenseFor list
        let! index = Gen.indexIn list
        return list |> List.insertAt index expense
    }

let genInput =
    Gen.sized (fun size ->
        gen {
            let! triplet = genTripletList

            let! pair =
                tryFind2020Pair triplet
                |> Option.map Gen.constant
                |> Option.defaultWith (fun () -> genPairList triplet)

            let pairProduct = pair |> List.mult
            let tripletProduct = triplet |> List.mult

            let coreList = List.append triplet pair
            let! list = Gen.repeat coreList size genLargerList

            return
                { Report = Entries list
                  PairProduct = pairProduct
                  TripletProduct = tripletProduct }
        })

type ArbMockPuzzleInput =
    static member Default() = Arb.fromGen genInput
