using Autofac;
using NetParty.Clients;
using NetParty.Clients.Interfaces;
using System.Configuration;

namespace NetParty.Application.DI
{
    public class ClientsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TesonetClient(
                    ConfigurationManager.ConnectionStrings["tesonetApi"].ConnectionString
                ))
                .As<ITesonetClient>()
                .SingleInstance();
        }
    }
}
