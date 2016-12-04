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

    // get uniq actor by name
    let counter = GrainClient.GrainFactory.GetGrain<ICounter>("realtime-consistent-counter")
    
    let writeJob() = task {
        Console.ForegroundColor <- ConsoleColor.Red 
        printfn "\n send Increment message which should take 5 sec to finish. \n"
        do! counter <! Increment // this message is not reentrant which means blocking operation

        Console.ForegroundColor <- ConsoleColor.Red
        printfn "\n send Increment message which should take 5 sec to finish. \n"
        do! counter <! Increment 

        Console.ForegroundColor <- ConsoleColor.Red
        printfn "\n send Increment message which should take 5 sec to finish. \n"
        do! counter <! Increment 
    }

    let readJob() = task {      
        let mutable count = 0
        
        while count < 3 do         
            let! result = counter <? GetCount // this message is reentrant which means not blocking operation
            count <- result
                    
            Console.ForegroundColor <- ConsoleColor.Yellow
            printfn " current value is %d" count
            do! Task.delay(TimeSpan.FromSeconds(0.5))

        Console.ForegroundColor <- ConsoleColor.Green
        printfn "\n job is finished."
    }

    writeJob() |> ignore
    Task.run(readJob) |> ignore

    Console.ReadLine() |> ignore
    0