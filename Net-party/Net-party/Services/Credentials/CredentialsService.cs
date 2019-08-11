using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;
using Net_party.Repositories;
using Net_party.Services.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Net_party.Services.Credentials
{
    public class CredentialsService  : ICredentialsService
    {
        private readonly CredentialsRepository _credentialsRepository;

        public CredentialsService()
        {
            _credentialsRepository = new CredentialsRepository();
        }

        public Task SaveUserInStorage(CredentialsDto userConfig)
        {
            return _credentialsRepository.SaveUser(UserCredentials.FromConfig(userConfig));
        }

        public async Task<UserCredentials> GetUser()
        {
            return await _credentialsRepository.GetLast();
        }

        public async Task<string> GetAuthorizationToken(UserCredentials userConfig)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri($"{ConfigurationManager.AppSettings["baseApiAddress"]}")
                };
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", userConfig.Username),
                    new KeyValuePair<string, string>("password", userConfig.Password)
                });
                var result = await client.PostAsync("tokens", content);
                var resultContent = await result.Content.ReadAsStringAsync();
                var jsonObject = (JObject) JsonConvert.DeserializeObject(resultContent);
                var tokenValue = jsonObject.GetValue("token");
                return tokenValue.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
