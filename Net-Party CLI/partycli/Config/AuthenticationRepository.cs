using Newtonsoft.Json;
using Unity;
using System;
using System.Threading.Tasks;
using partycli.Http;
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
            string content = JsonConvert.SerializeObject(new Credentials(Encrypt(username), Encrypt(password)));
            return m_repositoryProvider.SaveAsync(content);
        }

        public async Task<Credentials> LoadCredentialsAsync()
        {
            dynamic o = JsonConvert.DeserializeObject<Credentials>(await m_repositoryProvider.LoadAsync());
            return new Credentials(Decrypt(o.Username), Decrypt(o.Password));
        }

        public async Task<string> RetrieveToken()
        {
            var credentials = await LoadCredentialsAsync();
            var result = await m_httpService.PostJson(JsonConvert.SerializeObject(credentials));
            return result.token;
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
