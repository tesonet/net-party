using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Net_party.CommandLineModels;
using Net_party.Database;
using Net_party.Entities;
using Ninject;

namespace Net_party.Services
{
    class ServerService
    {
        public IEnumerable<Server> GetLocalServers()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var storage = kernel.Get<IStorage>();
            return storage.GetServers();
        }

    }
}
