using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Clients
{
    public interface IAuthorizationClient
    {
        Task<string> FetchToken(Credentials credentials);
    }
}