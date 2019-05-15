using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Services
{
    public interface IConfigurationService
    {
        Task StoreCredentials(Credentials credentials);
    }
}