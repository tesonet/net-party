using System.Threading.Tasks;
using NetParty.Domain.Models;

namespace NetParty.Domain.Interfaces
{
    public interface ILocalConfigurationRepository
    {
        Task<AuthorizationData> GetAuthorizationData();
        Task SaveAuthorizationData(AuthorizationData authorizationData);
    }
}