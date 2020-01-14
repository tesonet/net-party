using System.Threading.Tasks;

namespace net_party.Services.Contracts
{
    public interface IAuthService
    {
        Task AuthenticateCredentials(string username, string password);
    }
}