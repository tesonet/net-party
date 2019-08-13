using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Net_party.Database;
using Ninject;

namespace Net_party.Repositories.Server
{
    class ServerRepository : IServerRepository
    {
        private readonly IStorage _storage;

        public ServerRepository()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _storage = kernel.Get<IStorage>();
        }

        public Task SaveServers(List<Entities.Server> servers)
        {
            var context = _storage.GetContext();
            var serverTable = context.GetTable<Entities.Server>();
            DeleteServers();

            for (int i = 0; i < servers.Count; i++)
            {
                servers[i].Id = i; // Turns out sqlite is horrible with linq. Leaving the hack this way, could've went with raw sql queries, but decided that the hack is a bit more i
                serverTable.InsertOnSubmit(servers[i]);
                context.SubmitChanges();
            }

            return Task.CompletedTask;
        }

        public Task<List<Entities.Server>> GetServers()
        {
            using (var context = _storage.GetContext())
            {
                return Task.FromResult(context.GetTable<Entities.Server>().ToList());
            }
        }

        public Task DeleteServers()
        {
            using (var context = _storage.GetContext())
            {
                var table = context.GetTable<Entities.Server>();
                var serversToDelete = GetServers().Result;
                table.AttachAll(serversToDelete);
                table.DeleteAllOnSubmit(serversToDelete);
                context.SubmitChanges();
                return Task.CompletedTask;
            }
        }

    }
}
