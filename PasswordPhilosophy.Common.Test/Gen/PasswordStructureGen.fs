module AdventOfCode20.PasswordPhilosophy.PasswordStructureGen

open Microsoft.FSharp.Collections

type IsPolicyLetter = bool

type PasswordStructure = IsPolicyLetter list

open AdventOfCode20
open FsCheck

[<Literal>]
let private MaxPasswordLength = 20

let private indicesOfInterestOf policy =
    (policy.MinCount - 1, policy.MaxCount - 1)

let genStructureFor policy matchesDay1 matchesDay2 : Gen<PasswordStructure> =
    gen {
        let minCount, maxCount = (policy.MinCount, policy.MaxCount)
        let indexA, indexB = indicesOfInterestOf policy
        let indicesOfInterest = Set.ofList [ indexA; indexB ]

        let! policyLetterCount =
            if matchesDay1 && not matchesDay2 then
                Gen.choose (minCount, maxCount)
            elif not matchesDay1 && matchesDay2 then
                Gen.oneof [ if minCount > 1 then Gen.choose (1, minCount - 1)
                            Gen.choose (maxCount + 1, maxCount + 5) ]
            elif matchesDay1 && matchesDay2 then
                Gen.choose (minCount, maxCount)
            else // not matchesDay1 && not matchesDay2
                Gen.oneof [ Gen.choose (0, minCount - 1)
                            Gen.choose (maxCount + 1, maxCount + 5) ]

        let! requiredIndices, forbiddenIndices =
            if matchesDay2 then
                Gen.elements [ (Set.singleton indexA, Set.singleton indexB) // Only first
                               (Set.singleton indexB, Set.singleton indexA) ] // Only second
            else
                Gen.elements [ (Set.empty, indicesOfInterest) // Neither
                               if policyLetterCount >= 2 then
                                   (indicesOfInterest, Set.empty) ] // Both

        let requiredCount = Set.count requiredIndices
        let forbiddenCount = Set.count forbiddenIndices

        let minLength =
            max
                (if not (requiredIndices |> Set.isEmpty) then
                    max
                        (policyLetterCount + forbiddenCount)
                        ((requiredIndices |> Set.maxElement) + 1)
                else
                    policyLetterCount + forbiddenCount)
                MinPasswordLength

        let! length = Gen.choose (minLength, MaxPasswordLength)

        let! policyLetterIndices =
            List.init length id
            |> List.except requiredIndices
            |> List.except forbiddenIndices
            |> Gen.elements
            |> Gen.setWithCount (policyLetterCount - requiredCount)
            |> Gen.map (Set.union requiredIndices)

        return!
            Gen.initList length (fun index ->
                if policyLetterIndices |> Set.contains index then
                    Gen.constant true
                else
                    Gen.constant false)
    }
