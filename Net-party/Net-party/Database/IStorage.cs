using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;

namespace Net_party.Database
{
    public interface IStorage
    {
        Task SaveUser(CredentialsDto userConfig);
        Task GetUser();
        Task SaveServers(IEnumerable<ServersRetrievalConfigurationDto> servers);
        Task<IEnumerable<Server>> GetServers();
    }
}
