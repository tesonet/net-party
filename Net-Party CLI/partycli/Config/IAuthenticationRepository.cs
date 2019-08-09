using partycli.Helpers;
using System.Threading.Tasks;

namespace partycli.Config
{
    interface IAuthenticationRepository
    {
        Task SaveCredentialsAsync(string username, string password);

        Task<IRequestResult<string>> RetrieveToken();
    }
}