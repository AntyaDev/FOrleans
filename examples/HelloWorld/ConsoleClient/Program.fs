open System
open Orleans
open Orleans.Runtime.Configuration
open Orleans.FSharp
open Contracts

[<EntryPoint>]
let main argv = 
    
    printfn "Running demo...\n"

    let config = ClientConfiguration.LocalhostSilo()
    config.TraceToConsole <- false
    GrainClient.Initialize(config)

    let actor = GrainClient.GrainFactory.GetGrain<IGreeter>("test_actor")

    let job() = task {
        do! actor <! Greet "AntyaDev" // tell        
        let! name = actor <? GetName  // ask
        printfn "%s" name
        //do! actor <! "string type" won't compile
    }

    Task.run(job) |> ignore
    Console.ReadLine() |> ignore    
    0