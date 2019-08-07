using System.IO;
using System.Threading.Tasks;
using NetParty.Domain.Exceptions;
using NetParty.Domain.Interfaces;
using NetParty.Domain.Models;
using Newtonsoft.Json;

namespace NetParty.Infrastructure.Repositories
{
    public class LocalConfigurationRepository : ILocalConfigurationRepository
    {
        public const string AuthorizationFilePath = "./auth.txt";

        public Task<AuthorizationData> GetAuthorizationData()
        {
            if (!File.Exists(AuthorizationFilePath)) throw DomainExceptions.NotAuthorized;

            var fileContents = File.ReadAllText(AuthorizationFilePath);
            var authData = JsonConvert.DeserializeObject<AuthorizationData>(fileContents);

            return Task.FromResult(authData);
        }

        public Task SaveAuthorizationData(AuthorizationData authorizationData)
        {
            var authDataJson = JsonConvert.SerializeObject(authorizationData);
            File.WriteAllText(AuthorizationFilePath, authDataJson);

            return Task.CompletedTask;
        }
    }
}