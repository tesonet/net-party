using System.Threading.Tasks;
using Net_party.Entities;

namespace Net_party.Repositories.Credentials
{
    interface ICredentialsRepository
    {
        Task SaveUser(UserCredentials entity);

        Task<UserCredentials> GetLast();
    }
}
