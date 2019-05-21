using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Repositories.Core
{
    public interface ICredentialsRepository
    {
        Task SaveCredentialsAsync(Credentials credentials);
        Task<Credentials> GetCredentialsAsync();
    }
}