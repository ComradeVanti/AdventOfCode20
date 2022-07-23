namespace AdventOfCode20.TobogganTrajectory

open AdventOfCode20

type Slope = V2<int>

type CollisionCounts = Map<Slope, int>

type MockPuzzleInput = {
    Forest: Forest
    Collisions: CollisionCounts
}