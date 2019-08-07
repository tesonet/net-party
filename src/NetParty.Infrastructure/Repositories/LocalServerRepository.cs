using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NetParty.Domain.Exceptions;
using NetParty.Domain.Interfaces;
using NetParty.Domain.Models;
using Newtonsoft.Json;

namespace NetParty.Infrastructure.Repositories
{
    public class LocalServerRepository : ILocalServerRepository
    {
        public const string ServerFilePath = "./servers.txt";

        public Task<IEnumerable<Server>> Get()
        {
            if (!File.Exists(ServerFilePath)) throw DomainExceptions.NoLocalServersFound;

            var fileContents = File.ReadAllText(ServerFilePath);

            var servers = JsonConvert.DeserializeObject<IEnumerable<Server>>(fileContents);

            return Task.FromResult(servers);
        }

        public Task Save(IEnumerable<Server> servers)
        {
            var serversJson = JsonConvert.SerializeObject(servers);
            File.WriteAllText(ServerFilePath, serversJson);

            return Task.CompletedTask;
        }
    }
}