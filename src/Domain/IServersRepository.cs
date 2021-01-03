namespace Tesonet.ServerListApp.Domain
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServersRepository
    {
        Task<IEnumerable<Server>> GetAll();

        Task Store(IEnumerable<Server> servers);
    }
}