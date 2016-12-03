using System.Threading.Tasks;

namespace Orleans.FSharp.Core
{
    public interface IFGrain<TMsg> : IGrain
    {
        Task<object> Tell(TMsg message);
    }    
}
