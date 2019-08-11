using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Net_party.Database;
using Net_party.Logging;
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
            int i = 0;
            foreach (var server in servers)
            {
                server.Id = i++; // todo Fix the hack
                serverTable.InsertOnSubmit(server);
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
