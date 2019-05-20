using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Services.Interfaces
{
    public interface ICredentialsService
    {
        Task SaveCredentialsAsync(Credentials credentials);
        Task<Credentials> GetCredentialsAsync();
    }
}