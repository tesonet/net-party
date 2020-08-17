using System.Collections.Generic;
using System.Net.Http;
using LightInject;
using NetParty.Application.Commands;
using NetParty.Data;
using NetParty.Data.Files;
using NetParty.Data.Http;
using NetParty.Domain.Servers;
using NetParty.Domain.User;

namespace NetParty.Application
{
    internal class Container
    {
        public static ServiceContainer Build()
        {
            var container = new ServiceContainer();

            container.Register<IRepository<byte[]>>(c => new RawFileRepository("crd"), "crd_raw");
            container.Register<IRepository<byte[]>>(c => new RawFileRepository("srv"), "srv_raw");
            container.Register<ICredentialService>(c => new EncryptedCredentialService(c.GetInstance<IRepository<byte[]>>("crd_raw")));

            container.Register(c => new HttpClient());
            container.Register<ITokenProvider>(c => new TokenProvider(
                c.GetInstance<HttpClient>(),
                "https://playground.tesonet.lt/v1/tokens",
                c.GetInstance<ICredentialService>())
            );
            container.Register<IReadOnlyRepository<ICollection<Server>>>(c => new RestRepository<ICollection<Server>>(
                c.GetInstance<HttpClient>(),
                c.GetInstance<ITokenProvider>(),
                "https://playground.tesonet.lt/v1/servers")
            );
            container.Register<IRepository<ICollection<Server>>>(
                c => new JsonFileRepository<ICollection<Server>>(c.GetInstance<IRepository<byte[]> > ("srv_raw")));
            container.Register<IServerService>(c => new DefaultServerService(
                    c.GetInstance<IReadOnlyRepository<ICollection<Server>>>(),
                container.GetInstance<IRepository<ICollection<Server>>>())
            );

            container.Register<ICommand>(c => new ListServersCommand(c.GetInstance<IServerService>()), nameof(ListServersCommand));
            container.Register<ICommand>(c => new ConfigurationCommand(c.GetInstance<ICredentialService>()), nameof(ConfigurationCommand));

            return container;
        }
    }
}
