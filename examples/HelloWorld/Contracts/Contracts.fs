namespace Contracts

open Orleans
open FOrleans.Core

type Message = 
    | Greet of string    
    | GetName

type IGreeter = 
    inherit IFGrain<Message>
    inherit IGrainWithStringKey
