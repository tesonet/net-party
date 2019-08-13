using System.Threading.Tasks;
using Net_party.Entities;

namespace Net_party.Services.Credentials
{
    interface ICredentialsService
    {
        Task SaveUserInStorage(CommandLineModels.Credentials userConfig);
        Task<UserCredentials> GetUser();
        Task<string> GetAuthorizationToken(UserCredentials userConfig);
    }
}
