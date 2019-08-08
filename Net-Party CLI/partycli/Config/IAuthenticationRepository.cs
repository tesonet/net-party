using System.Threading.Tasks;

namespace partycli
{
    interface IAuthenticationRepository
    {
        Task SaveCredentialsAsync(string username, string password);

        Task<Credentials> LoadCredentialsAsync();
    }
}
