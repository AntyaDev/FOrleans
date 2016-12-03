namespace Grains

open Orleans
open Orleans.FSharp
open Contracts

type Greeter() =
    inherit FGrain<Message>()
    interface IGreeter

    override this.Receive message = task {
        match message with
        | Greet who -> printfn "received a new message: Greet from %s" who                       
                       return response()
        
        | GetName   -> printfn "received a new message: GetName"
                       return response("My name is Greeter!!!")
    }