using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Services
{
    public interface IOutputService
    {
        Task OutputServers(List<Server> servers);
        Task OutputStringLine(string text);
    }
}