#region Using

using Autofac;
using NetParty.Application.Servers;
using NetParty.Application.Servers.ServerListApi;
using NetParty.Application.StorageProvider;
using Pathoschild.Http.Client;

#endregion

namespace NetParty.Application.DependencyInjection.Modules
    {
    public class ServersModule : Module
        {
        protected override void Load(ContainerBuilder builder)
            {
            builder.Register(c => new FluentClient("http://playground.tesonet.lt/v1/")).As<IClient>();
            builder.RegisterType<ServerListApi>().As<IRemoteServerProvider>();
            builder.RegisterType<ServerListApiTokenProvider>().As<IServerListApiTokenProvider>();

            builder.RegisterType<LocalServerRepository>().As<IServerRepository>().As<IServerProvider>();
            builder.Register(c => new FileStorageProvider("servers.dat")).As<IServerStorageProvider>();

            builder.RegisterType<ConsoleServerDisplayer>().As<IServerDisplayer>();
            }
        }
    }
