using Newtonsoft.Json;
using Unity;
using System;
using System.Threading.Tasks;
using partycli.Http;
using partycli.Helpers;
using partycli.Repository;

namespace partycli.Config
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IRepositoryProvider m_repositoryProvider;
        private readonly IHttpService m_httpService;

        [InjectionConstructor]
        public AuthenticationRepository(IHttpService httpService, IRepositoryProvider repositoryProvider)
        {
            m_repositoryProvider = repositoryProvider;
            m_httpService = httpService;
        }

        public Task SaveCredentialsAsync(string username, string password)
        {
            m_repositoryProvider.Reset();
            return m_repositoryProvider.SaveAsync(JsonConvert.SerializeObject(new Credentials(Encrypt(username), Encrypt(password))));
        }
        public async Task<IRequestResult<string>> RetrieveToken()
        {
            var response = await m_httpService.PostJson(JsonConvert.SerializeObject(await LoadCredentialsAsync()));
            if (response.Success)
                return new SuccessResult<string>(response.Result);
            return new FailedResult(response.ErrorMessage);
        }   

        private async Task<Credentials> LoadCredentialsAsync()
        {
            dynamic o = JsonConvert.DeserializeObject<Credentials>(await m_repositoryProvider.LoadAsync());
            return new Credentials(Decrypt(o.Username), Decrypt(o.Password));
        }

        public static string Encrypt(string s) { return Reverse(s); }
        public static string Decrypt(string s) { return Reverse(s); }
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
