using NetParty.Contracts.Requests.Base;
using System.Threading.Tasks;

namespace NetParty.Handlers.Base
{
    public interface IHandler<in TRequest>
        where TRequest : BaseRequest
    {
        Task HandleAsync(TRequest request);
    }
}
