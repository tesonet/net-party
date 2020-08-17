using System.Threading.Tasks;

namespace NetParty.Domain.User
{
    public interface ICredentialService
    {
        Task<Credentials> GetAsync();
        Task SaveAsync(Credentials credentials);
    }
}
