namespace Orleans.FSharp

[<AutoOpen>]
module FGrain =
    open System
    open System.Threading.Tasks
    open Orleans
    open Orleans.FSharp.Core
    open Orleans.FSharp.Task

    [<AbstractClass>]
    type FGrain<'TMsg>() = 
        inherit Grain()

        abstract Receive: message:'TMsg -> Task<obj>

        interface IFGrain<'TMsg> with            
            member this.Tell(message) = this.Receive(message)
    

    [<AbstractClass>]
    type FGrain<'TMsg, 'TState when 'TState: (new: unit -> 'TState)>() = 
        inherit Grain<'TState>()

        abstract Receive: message:'TMsg -> Task<obj>

        interface IFGrain<'TMsg> with            
            member this.Tell(message) = this.Receive(message)


    let inline response (data:obj) =
        match data with
        | :? unit as u -> null
        | _            -> data


    let inline (<?) (grain:IFGrain<'TMsg>) (message:'TMsg): Task<'TResponse> =
        grain.Tell(message) |> Task.map(fun r -> r :?> 'TResponse)
   
    let inline (<!) (grain:IFGrain<'TMsg>) (message:'TMsg) =         
        grain.Tell(message) |> Task.map(ignore)      

    let inline (<*) (grain:IFGrain<'TMsg>) (message:'TMsg) =         
        grain.Tell(message) |> ignore