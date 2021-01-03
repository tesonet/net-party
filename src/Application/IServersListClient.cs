namespace Tesonet.ServerListApp.Application
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IServersListClient
    {
        Task<IReadOnlyCollection<Domain.Server>> GetAll();
    }
}