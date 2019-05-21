using NetParty.Contracts;
using System.Threading.Tasks;

namespace NetParty.Repositories.Core
{
    public interface IServersRepository
    {
        Task<ServerDto[]> GetServersAsync();
        Task<ServerDto[]> SaveServersAsync(ServerDto[] data);
    }
}
