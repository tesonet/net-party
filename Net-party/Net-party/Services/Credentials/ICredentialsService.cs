using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;

namespace Net_party.Services.Config
{
    interface ICredentialsService
    {
        Task SaveUserInStorage(CredentialsDto userConfig);
        Task<UserCredentials> GetUser();
        Task<string> GetAuthorizationToken(UserCredentials userConfig);
    }
}
