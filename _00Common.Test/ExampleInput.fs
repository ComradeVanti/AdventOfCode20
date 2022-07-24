[<Microsoft.FSharp.Core.RequireQualifiedAccess>]
module AdventOfCode20.ExampleInput

open System.IO
open System.Reflection

let private resourceNameIn (assembly: Assembly) =
    assembly.GetManifestResourceNames()
    |> Array.find (fun name -> name.EndsWith("example-input.txt"))

let content assembly =
    let resourceName = resourceNameIn assembly

    using (assembly.GetManifestResourceStream(resourceName)) (fun stream ->
        using (new StreamReader(stream)) (fun reader -> reader.ReadToEnd()))

let lines assembly = content assembly |> String.splitAt '\n'
