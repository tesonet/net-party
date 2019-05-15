using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Repositories
{
    public interface ICredentialsRepository
    {
        Task SaveCredentials(Credentials credentials);
        Task<Credentials> LoadCredentials();
    }
}