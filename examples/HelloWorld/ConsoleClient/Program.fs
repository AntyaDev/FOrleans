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

    let greeter = GrainClient.GrainFactory.GetGrain<IGreeter>("test_greeter")

    let job() = task {
        do! greeter <! Greet "AntyaDev" // tell        
        let! name = greeter <? GetName  // ask
        printfn "%s" name
        //do! greeter <! "string type" won't compile
    }

    Task.run(job) |> ignore
    Console.ReadLine() |> ignore    
    0