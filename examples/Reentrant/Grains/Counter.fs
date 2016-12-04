namespace Grains

open System
open Orleans
open Orleans.Concurrency
open Orleans.CodeGeneration
open Orleans.FSharp
open Contracts

[<MayInterleave("IsReentrant")>]
type Counter() =
    inherit FGrain<Message>()

    let mutable count = 0

    interface ICounter

    override this.Receive message = task {
        match message with
        | Increment -> do! Task.delay(TimeSpan.FromSeconds(5.0)) // write to database a new value, IO bound blocking operation
                       count <- count + 1
                       return response()

        | Decrement -> do! Task.delay(TimeSpan.FromSeconds(5.0)) // write to database a new value, IO bound blocking operation
                       count <- count - 1
                       return response()

        | GetCount -> return response(count) // reentrant operation, is not blocking.
    }

    static member IsReentrant(req:InvokeMethodRequest) = 
        match req.Arguments.[0] with
        | :? Message as m -> match m with                             
                             | GetCount -> true  // here we say that GetCount is reentrant message.
                             | _        -> false
        | _ -> false


