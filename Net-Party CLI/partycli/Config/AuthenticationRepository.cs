using Newtonsoft.Json;
using Unity;
using System;
using System.Threading.Tasks;

namespace partycli
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private IRepositoryProvider m_repositoryProvider;

        [InjectionConstructor]
        public AuthenticationRepository(IRepositoryProvider repositoryProvider)
        {
            m_repositoryProvider = repositoryProvider;
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
