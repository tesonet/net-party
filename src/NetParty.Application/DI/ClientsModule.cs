using Autofac;
using NetParty.Clients;
using NetParty.Clients.Interfaces;

namespace NetParty.Application.DI
{
    public class ClientsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TesonetClient("http://playground.tesonet.lt"))
                .As<ITesonetClient>()
                .SingleInstance();
        }
    }
}
