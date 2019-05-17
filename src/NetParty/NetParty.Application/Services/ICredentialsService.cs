using System.Threading.Tasks;

namespace NetParty.Application
{
    public interface ICredentialsService
    {
        Task SaveCredentialsAsync(string userName, string userPassword);
    }
}