using System;
using System.Threading.Tasks;
using Orleans;

namespace FOrleans.Core
{
    public interface IFGrain<TMsg> : IGrain
    {
        Task<object> Tell(TMsg message);
    }    
}
