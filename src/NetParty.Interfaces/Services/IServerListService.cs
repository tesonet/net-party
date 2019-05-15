using System.Threading.Tasks;

namespace NetParty.Interfaces.Services
{
    public interface IServerListService
    {
        Task PrintServerList(bool local);
    }
}