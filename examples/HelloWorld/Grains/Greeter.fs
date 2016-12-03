namespace Grains

open Orleans
open FOrleans
open Contracts

type Greeter() =
    inherit FGrain<Message>()
    interface IGreeter

    override this.Receive message = task {
        match message with
        | Greet who -> printfn "received new message: Greet from %s" who
                       let msg = sprintf "Hello %s" who
                       return response(msg)
        
        | GetName   -> printfn "received new message: GetName"
                       return response("My name is Greeter!!!")
    }