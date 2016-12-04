namespace Contracts

open Orleans
open Orleans.FSharp.Core

type Message =
    | Increment
    | Decrement
    | GetCount

type ICounter =
    inherit IFGrain<Message>
    inherit IGrainWithStringKey