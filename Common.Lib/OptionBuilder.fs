[<Microsoft.FSharp.Core.AutoOpen>]
module AdventOfCode20.OptionBuilder

open System

type OptionBuilder() =
    member this.Bind (x, f) = Option.bind f x
    member this.Return x = Some x
    member this.ReturnFrom (x: Option<_>) = x
    member this.Zero () = None
    member this.Delay f = f
    member this.Run f = f ()
    member this.Combine (x, f) = Option.orElseWith f x

    member this.TryWith (body, handler) =
        try this.ReturnFrom (body ())
        with e -> handler e

    member this.TryFinally (body, compensation) =
        try this.ReturnFrom (body ())
        finally compensation ()

    member this.Using (disposable: #IDisposable, body) =
        let body' () = body disposable
        this.TryFinally(body', fun () -> disposable.Dispose())
        
let opt = OptionBuilder()