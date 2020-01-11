using net_party.Entities.Database;
using System.Threading.Tasks;

namespace net_party.Services.Contracts
{
    public interface IAuthService
    {
        Task<AuthToken> AcquireNewToken();
    }
}