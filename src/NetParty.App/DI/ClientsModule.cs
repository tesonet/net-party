using Autofac;
using NetParty.Clients;
using NetParty.Interfaces.Clients;
using Pathoschild.Http.Client;

namespace NetParty.App.DI
{
    public class ClientsModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AuthorizationClient(new FluentClient("http://playground.tesonet.lt")))
                .As<IAuthorizationClient>();
            builder.Register(c => new ServersClient(new FluentClient("http://playground.tesonet.lt")))
                .As<IServersClient>();
        }
    }
}
