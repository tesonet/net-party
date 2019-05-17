using System.Threading.Tasks;

namespace NetParty.Application.Handlers.Base
{
    interface IHandler<TRequest>
        where TRequest : class
    {
        Task HandleAsync(TRequest request);
    }
}
