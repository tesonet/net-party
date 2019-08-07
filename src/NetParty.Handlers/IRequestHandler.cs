using System.Threading.Tasks;

namespace NetParty.Handlers
{
    public interface IRequestHandler<in TRequest>
    {
        Task ThenAsync(TRequest request);
    }

    public interface IRequestHandler<in TRequest,TResult>
    {
        Task<TResult> ThenAsync(TRequest request);
    }
}