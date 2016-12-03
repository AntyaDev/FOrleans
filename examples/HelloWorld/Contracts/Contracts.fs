namespace Contracts

open Orleans
open Orleans.FSharp.Core

type Message = 
    | Greet of string    
    | GetName

type IGreeter = 
    inherit IFGrain<Message>
    inherit IGrainWithStringKey
